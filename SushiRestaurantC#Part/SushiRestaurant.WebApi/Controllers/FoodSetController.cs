using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.WebApi.Dtos.FoodSets;

namespace SushiRestaurant.WebApi.Controllers;

public class FoodSetController : Controller
{
    private readonly IFoodSetService _foodSetService;

    public FoodSetController(IFoodSetService foodSetService)
    {
        _foodSetService = foodSetService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = await _foodSetService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var foodSet = await _foodSetService.GetAsync(id, cancellationToken);
        if (foodSet is null)
            return NotFound();

        return Ok(foodSet);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateFoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = new FoodSet
        {
            Name = dto.Name,
            Category = dto.Category,
            Cost = dto.Cost,
            Description = dto.Description,
            Dishes = dto.Dishes,
            ImageURL = dto.ImageURL,
        };
        var id = await _foodSetService.CreateAsync(foodSet, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateFoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = new FoodSet
        {
            Id = dto.Id,
            Name = dto.Name,
            Category = dto.Category,
            Cost= dto.Cost,
            Description = dto.Description,
            Dishes = dto.Dishes,
            ImageURL = dto.ImageURL,
        };
        await _foodSetService.UpdateAsync(foodSet, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _foodSetService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
