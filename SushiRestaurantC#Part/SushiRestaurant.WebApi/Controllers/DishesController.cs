using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.WebApi.Dtos.Dishes;

namespace SushiRestaurant.WebApi.Controllers;

public class DishesController : Controller
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = await _dishService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var dish = await _dishService.GetAsync(id, cancellationToken);
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }

    [HttpGet("{categoryName}")]
    public async Task<IActionResult> Get([FromRoute] string categoryName, CancellationToken cancellationToken)
    {
        var dish = _dishService.GetAllDishesByCategory(categoryName, cancellationToken);
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }



    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateDishDto dto, CancellationToken cancellationToken)
    {
        var dish = new Dish
        {
            Category = dto.Category,
            Cost = dto.Cost,
            Description = dto.Description,
            ImageURL = dto.ImageURL,
            Name = dto.Name
        };
        var id = await _dishService.CreateAsync(dish, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDishDto dto, CancellationToken cancellationToken)
    {
        var dish = new Dish
        {
            Id = dto.Id,
            Category = dto.Category,
            Cost = dto.Cost,
            Description = dto.Description,
            ImageURL = dto.ImageURL,
            Name = dto.Name
        };
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
