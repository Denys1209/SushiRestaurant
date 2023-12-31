using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.FoodSets;
using AutoMapper;
using SushiRestaurant.WebApi.Dtos;

namespace SushiRestaurant.WebApi.Controllers;


public class FoodSetController : Controller
{
    private readonly IFoodSetService _foodSetService;
    private readonly IMapper _mapper;

    public FoodSetController(IFoodSetService foodSetService, IMapper mapper)
    {
        _foodSetService = foodSetService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = await _foodSetService.GetAllAsync(paginationDto, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var foodSet = await _foodSetService.GetAsync(id, cancellationToken);
        if (foodSet is null)
            return NotFound();

        return Ok(_mapper.Map<FoodSetDto>(foodSet));
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] FoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = _mapper.Map<FoodSet>(dto);
        var id = await _foodSetService.CreateAsync(foodSet, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] FoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = _mapper.Map<FoodSet>(dto);
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
