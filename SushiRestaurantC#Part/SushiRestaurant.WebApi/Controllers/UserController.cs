using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;

namespace SushiRestaurant.WebApi.Controllers;
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
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

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        var id = await _userService.CreateAsync(user, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
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

    //[HttpGet("exists")]
    //public async Task<IActionResult> CheckUserExists([FromQuery] string email, CancellationToken cancellationToken)
    //{
    //    var exists = await _userService.CheckUserExistsAsync(email, cancellationToken);
    //    return Ok(new { exists });
    //}
}