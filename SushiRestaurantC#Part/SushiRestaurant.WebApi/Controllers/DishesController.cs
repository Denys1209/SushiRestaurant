﻿using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using SushiRestaurant.Application.Dishes;
using AutoMapper;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.WebApi.Dtos.Dish;
using SushiRestaurant.Application.UserDishes;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.DishDtos;

namespace SushiRestaurant.WebApi.Controllers;

public class DishesController : Controller
{
    private readonly IDishService _dishService;
    private readonly ICategoryService _categoryService;
    private readonly IUserDishService _userDishService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public DishesController(IDishService dishService, ICategoryService categoryService, IUserDishService userDishService, IUserService userService, IMapper mapper)
    {
        _dishService = dishService;
        _categoryService = categoryService;
        _userDishService = userDishService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var dishes = _mapper.Map<List<GetDishDto>>(await _dishService.GetAllAsync(paginationDto, cancellationToken));
        var numberOfPages = await _dishService.GetNumberOfPagesAsync(paginationDto.PageSize, cancellationToken);
        return Ok(new ReturnDishPageDto { Dishes = dishes, HowManyPages = numberOfPages });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<GetDishDto>(await _dishService.GetAsync(id, cancellationToken));
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }

    [HttpGet("{categoryName}")]
    public async Task<IActionResult> Get([FromRoute] string categoryName, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<List<GetDishDto>>(await _dishService.GetAllDishesByCategoryAsync(categoryName, cancellationToken));
        if (dish is null)
            return NotFound();

        return Ok(dish);
    }



    [HttpPost("createDish")]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromQuery] int categoryId, [FromBody] CreateDishDto dto, CancellationToken cancellationToken)
    {

        var dish = _mapper.Map<Dish>(dto);
        Category? category = await _categoryService.GetAsync(categoryId, cancellationToken);
        if (category is null)
        {
            ModelState.AddModelError("", $"category with the {categoryId} id deosn't exist");
            return StatusCode(422, ModelState);
        }
        dish.Category = category;
        var id = await _dishService.CreateAsync(dish, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateDishDto dto, CancellationToken cancellationToken)
    {
        Category? category = await _categoryService.GetAsync(dto.CategoryId, cancellationToken);
        if (category is null) 
        {
            ModelState.AddModelError("", $"category with the {dto.CategoryId} id deosn't exist");
            return StatusCode(422, ModelState);
        }
        var dish = _mapper.Map<Dish>(dto);
        dish.Category = category;
        await _dishService.UpdateAsync(dish, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _dishService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("multiple")]
    public async Task<IActionResult> GetMultiple([FromQuery] List<int> ids, CancellationToken cancellationToken)
    {
        var dishes = await _dishService.GetAllModelsByIdsAsync(ids, cancellationToken);
        if (dishes == null || !dishes.Any())
            return NotFound();

        var dishDtos = _mapper.Map<List<GetDishDto>>(dishes);
        return Ok(dishDtos);
    }

    [HttpGet("favoriteDishes")]
    public async Task<IActionResult> GetAllFavoriteDishesForUser([FromQuery] int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(id, cancellationToken);
        if (user == null) return NotFound();
        var favoriteDishes = _mapper.Map<List<GetDishDto>>(await _userDishService.GetAllFavoriteDishesOfUserAsync(user, cancellationToken));
        return Ok(favoriteDishes);
    }

}
