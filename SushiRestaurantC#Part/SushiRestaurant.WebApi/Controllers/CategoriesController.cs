using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;
using AutoMapper;
using SushiRestaurant.WebApi.Dtos.Categories;
using SushiRestaurant.WebApi.Dtos.CategoryDtos;

namespace SushiRestaurant.WebApi.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var categories = _mapper.Map<List<GetCategoryDto>>(await _categoryService.GetAllAsync(paginationDto, cancellationToken));
        var numberOfPages = await _categoryService.GetNumberOfPagesAsync(paginationDto.PageSize, cancellationToken);
        return Ok(
            new ReturnPageDto { categories=categories, HowManyPages=numberOfPages }
            );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<GetCategoryDto>(await _categoryService.GetAsync(id, cancellationToken));
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(dto);
        var id = await _categoryService.CreateAsync(category, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {

        var category = _mapper.Map<Category>(dto);
        await _categoryService.UpdateAsync(category, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("multiple")]
    public async Task<IActionResult> GetMultiple([FromQuery] List<int> ids, CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllModelsByIdsAsync(ids, cancellationToken);
        if (categories == null || !categories.Any())
            return NotFound();

        var categoryDtos = _mapper.Map<List<GetCategoryDto>>(categories);
        return Ok(categoryDtos);
    }

}

