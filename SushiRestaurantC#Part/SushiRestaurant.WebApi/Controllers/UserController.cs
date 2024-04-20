using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.UserDishes;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRestaurant.WebApi.EmailService;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SushiRestaurant.WebApi.Controllers;
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IDishService _dishService;
    private readonly IUserDishService _userDishService;
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IUserDishService userDishService, IConfiguration configuration, IEmailSender emailSender, IDishService dishService, IMapper mapper)
    {
        _userService = userService;
        _userDishService = userDishService;
        _configuration = configuration;
        _emailSender = emailSender;
        _dishService = dishService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var users = _mapper.Map<List<GetUserDto>>(await _userService.GetAllAsync(paginationDto, cancellationToken));
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<GetUserDto>(await _userService.GetAsync(id, cancellationToken));
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto model, CancellationToken cancellation)
    {

        string token = CreateRandomToken();
        var user = new User(
            model.Email,
            model.Password,
            model.Username,
            Constants.Constants.UserRoleString,
            token,
            DateTime.Now
        );
        if (await _userService.CheckUserExistsAsync(model.Email, cancellation))
        {
            return BadRequest("User already exist");
        }
        var result = await _userService.CreateAsync(user, cancellation);

        string verificationLink = GenerateVerificationLink(model.Email, token);

        _emailSender.SendEmail(new EmailSendRequest
        {
            Content = $"Please verify your account by clicking the following link: {verificationLink}",
            Subject = "Account Verification",
            To = model.Email,
        });

        return Ok(new { message = "User registered successfully" });
    }
    [HttpPost("registerAdmin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] CreateUserDto model, CancellationToken cancellation)
    {

        string token = CreateRandomToken();
        var user = new User(
            model.Email,
            model.Password,
            model.Username,
            Constants.Constants.AdminRoleString,
            token,
            DateTime.Now
        );
        if (await _userService.CheckUserExistsAsync(model.Email, cancellation))
        {
            return BadRequest("User already exist");
        }
        var result = await _userService.CreateAsync(user, cancellation);

        string verificationLink = GenerateVerificationLink(model.Email, token);

        _emailSender.SendEmail(new EmailSendRequest
        {
            Content = $"Please verify your account by clicking the following link: {verificationLink}",
            Subject = "Account Verification",
            To = model.Email,
        });

        return Ok(new { message = "User registered successfully" });
    }

    [HttpGet("verify")]
    public async Task<IActionResult> Verify(string email, string token, CancellationToken cancellation)
    {
        var user = await _userService.VerifyUser(email, token, cancellation);
        if (user == null)
        {
            return BadRequest("Invalid token or email.");
        }
        return Ok("User verified! :)");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
    {
        var user = await _userService.ForgetPassword(email, cancellationToken);
        if (user == null)
        {
            return BadRequest("User not found.");
        }
        _emailSender.SendEmail(new EmailSendRequest
        {
            Content = user.PasswordResetToken!,
            Subject = "Reset password token",
            To = user.Email,
        });

        return Ok("You may now reset your password.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.ResetPassword(request.Email, request.NewPassword, request.ResetCode, cancellationToken);
        if (user == null || user.ResetTokenExpires < DateTime.Now)
        {
            return BadRequest("Invalid Token or email or other");
        }

        return Ok("Password successfully reset.");
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        await _userService.UpdateAsync(user, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("exists")]
    public async Task<IActionResult> CheckUserExists([FromQuery] string email, CancellationToken cancellationToken)
    {
        var exists = await _userService.CheckUserExistsAsync(email, cancellationToken);
        return Ok(new { exists });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto model, CancellationToken cancellationToken)
    {
        var user = await _userService.ValidateUserAsync(model.Email, model.Password, cancellationToken);
        if (user is null)
        {
            return BadRequest("User doesn't exsist or isn't verify");
        }

        var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        var key = Encoding.ASCII.GetBytes(tokenOptions.AuthenticatorTokenProvider);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var favoriteDishesIds = await _userDishService.GetAllFavoriteDishesIdOfUserAsync(user, cancellationToken);
        LoginResualtUserDto loginResualtUserDto = new LoginResualtUserDto
        {
            Email = user.Email,
            Id = user.Id,
            Token = tokenHandler.WriteToken(token),
            Username = user.Username,
            FavoriteDishesIds = (ICollection<int>)favoriteDishesIds,
        };
        return Ok(loginResualtUserDto);
    }

    [HttpPost("addFavoriteDish")]
    public async Task<IActionResult> AddFavoriteDish(int userId, int dishId, CancellationToken cancellationToken)
    {

        var user = await _userService.GetAsync(userId, cancellationToken);
        if (user is null)
        {
            return BadRequest($"User with {userId} doesn't exist");
        }

        var dish = await _dishService.GetAsync(dishId, cancellationToken);
        if (dish is null)
        {
            return BadRequest($"Dish with {dishId} doesn't exist");
        }

        var result = await _userDishService.CreateAsync(new UserDish { Dish = dish, User = user }, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("deleteFavoriteDish")]
    public async Task<IActionResult> DeleteFavoriteDish(int userId, int dishId, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(userId, cancellationToken);
        if (user is null)
        {
            return BadRequest($"User with {userId} doesn't exist");
        }

        var dish = await _dishService.GetAsync(dishId, cancellationToken);
        if (dish is null)
        {
            return BadRequest($"Dish with {dishId} doesn't exist");
        }

        await _userDishService.DeleteFavoriteDishAsync(user, dish, cancellationToken);
        return Ok("favorite dish deleted");

    }



    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
    }

    private string GenerateVerificationLink(string email, string token)
    {
        string baseUrl = "https://localhost:7073";

        string verifyPath = "/api/user/verify";

        string verificationLink = $"{baseUrl}{verifyPath}?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}";

        return verificationLink;
    }

}