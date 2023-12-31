using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.Dishes;
using AutoMapper;
using SushiRestaurant.WebApi.Dtos;

namespace SushiRestaurant.WebApi.Controllers;

public class DishesController : Controller
{
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public DishesController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var dishes = _mapper.Map<List<DishDto>>(await _dishService.GetAllAsync(paginationDto, cancellationToken));
        return Ok(dishes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<DishDto>(await _dishService.GetAsync(id, cancellationToken));
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }

    [HttpGet("{categoryName}")]
    public async Task<IActionResult> Get([FromRoute] string categoryName, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<List<DishDto>>(_dishService.GetAllDishesByCategory(categoryName, cancellationToken));
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }



    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] DishDto dto, CancellationToken cancellationToken)
    {

        var dish = _mapper.Map<Dish>(dto);
        var id = await _dishService.CreateAsync(dish, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] DishDto dto, CancellationToken cancellationToken)
    {

        var dish = _mapper.Map<Dish>(dto);
        await _dishService.UpdateAsync(dish, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _dishService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

}
