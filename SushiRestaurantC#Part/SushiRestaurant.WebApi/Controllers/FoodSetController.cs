using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.FoodSets;
using AutoMapper;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.WebApi.Dtos.FoodSet;
using SushiRestaurant.WebApi.Dtos.FoodSets;
using SushiRestaurant.WebApi.Dtos.Dish;

namespace SushiRestaurant.WebApi.Controllers;


public class FoodSetController : Controller
{
    private readonly IFoodSetService _foodSetService;
    private readonly IDishService _dishService;
    private readonly IDishFoodSetService _dishesFoodSetsService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public FoodSetController(IFoodSetService foodSetService, IDishService dishService, ICategoryService categoryService, IDishFoodSetService dishFoodSetService, IMapper mapper)
    {
        _foodSetService = foodSetService;
        _dishService = dishService;
        _categoryService = categoryService;
        _dishesFoodSetsService = dishFoodSetService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var foodSets = _mapper.Map<List<GetFoodSetDto>>(await _foodSetService.GetAllAsync(paginationDto, cancellationToken));
        foreach (var foodSet in foodSets)
        {
            foodSet.Dishes = _mapper.Map<List<GetDishDto>>(await _dishService.GetAllDishesInFoodSetIdAsync(foodSet.Id, cancellationToken));
        }

        return Ok(foodSets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var foodSet = _mapper.Map<GetFoodSetDto>(await _foodSetService.GetAsync(id, cancellationToken));
        if (foodSet is null)
            return NotFound();
        foodSet.Dishes = _mapper.Map<List<GetDishDto>>(await _dishService.GetAllDishesInFoodSetIdAsync(foodSet.Id, cancellationToken));
        return Ok(_mapper.Map<GetFoodSetDto>(foodSet));
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromQuery] int categoryId, [FromQuery] List<int> dishesId, [FromBody] CreateFoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = _mapper.Map<FoodSet>(dto);
        var category = await _categoryService.GetAsync(categoryId, cancellationToken);
        if (category is null)
        {
            ModelState.AddModelError("", $"Category with {categoryId} id doesn't exist");
            return StatusCode(422, ModelState);
        }
        foodSet.Category = category;
        var dishes = (await _dishService.GetAllModelsByIdsAsync(dishesId, cancellationToken)).ToArray();
        for (var i = 0; i < dishesId.Count; ++i)
        {
            if (dishes[i] is null)
            {
                ModelState.AddModelError("", $"Dish with {dishesId[i]} doesn't exist");
                return StatusCode(422, ModelState);
            }
        }
        var id = await _foodSetService.CreateAsync(foodSet, cancellationToken);
        var createdFoodSet = await _foodSetService.GetAsync(id, cancellationToken);
        if (createdFoodSet is null)
        {
            ModelState.AddModelError("", $"FoodSet wasn't created");
            return StatusCode(422, ModelState);
        }
        foreach (var item in dishes)
        {
            await _dishesFoodSetsService.CreateAsync(new DishFoodSet { Dish = item!, FoodSet = createdFoodSet!, }, cancellationToken);
        }

        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] int categoryId, [FromQuery] List<int> dishesId, [FromBody] UpdateFoodSetDto dto, CancellationToken cancellationToken)
    {
        var foodSet = _mapper.Map<FoodSet>(dto);
        var category = await _categoryService.GetAsync(categoryId, cancellationToken);
        if (category is null)
        {
            ModelState.AddModelError("", $"Category with {categoryId} id doesn't exist");
            return StatusCode(422, ModelState);
        }
        foodSet.Category = category;
        var dishes = (await _dishService.GetAllModelsByIdsAsync(dishesId, cancellationToken)).ToArray();
        for (var i = 0; i < dishesId.Count; ++i)
        {
            if (dishes[i] is null)
            {
                ModelState.AddModelError("", $"Dish with {dishesId[i]} doesn't exist");
                return StatusCode(422, ModelState);
            }
        }
        await _foodSetService.UpdateAsync(foodSet, cancellationToken);
        var createdFoodSet = await _foodSetService.GetAsync(foodSet.Id, cancellationToken);
        if (createdFoodSet is null)
        {
            ModelState.AddModelError("", $"FoodSet wasn't found");
            return StatusCode(422, ModelState);
        }
        foreach (var item in createdFoodSet.DishFoodSets) 
        {
            await _dishesFoodSetsService.DeleteAsync(item.Id, cancellationToken);
        }
        foreach (var item in dishes)
        {
            await _dishesFoodSetsService.CreateAsync(new DishFoodSet { Dish = item!, FoodSet = createdFoodSet!, }, cancellationToken);
        }
       
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _foodSetService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
