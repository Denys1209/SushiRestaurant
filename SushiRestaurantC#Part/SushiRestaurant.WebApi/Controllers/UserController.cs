using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.UserDishes;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.Data;
using SushiRestaurant.WebApi.EmailService;
using SushiRestaurant.Application.Dishes;

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

        _emailSender.SendEmail(new EmailSendRequest {
            Content = token,
            Subject = "Registration",
            To = model.Email,
        });

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("verify")]
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
            return Unauthorized();
        }

        var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        var key = Encoding.ASCII.GetBytes(tokenOptions.AuthenticatorTokenProvider);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new { token = tokenHandler.WriteToken(token), id = user.Id });
    }

    [HttpPost]
    public async Task<IActionResult> AddFavoriteDish(int userId, int dishId, CancellationToken cancellationToken ) 
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

        var result = _userDishService.CreateAsync(new UserDish { Dish = dish, User = user }, cancellationToken);

        return Ok(result);
    }



    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
    }
}