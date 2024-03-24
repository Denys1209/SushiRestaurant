using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.UserDishes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SushiRestaurant.WebApi.Controllers;
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserDishService _userDishService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IUserDishService userDishService, IConfiguration configuration, IMapper mapper)
    {
        _userService = userService;
        _userDishService = userDishService;
        _configuration = configuration;
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
      
        var user = new User(model.Email, model.Password, model.Username, Constants.Constants.UserRoleString);
        var result = await _userService.CreateAsync(user, cancellation);

        return Ok(new { message = "User registered successfully" });
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

        return Ok(new { token = tokenHandler.WriteToken(token) });
    }

}