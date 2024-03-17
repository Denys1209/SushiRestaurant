using Microsoft.AspNetCore.Mvc;
using MVC.SushiRestaurant.ViewModels;
using SushiRestaurant.Application.Categories;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;


namespace MVC.SushiRestaurant.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoriesService;


    public CategoriesController(ICategoryService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var dto = new FilterPaginationDto(string.Empty, 0, 50, "Id",
                SortOrder.Asc);

        var categories = await _categoriesService.GetAllAsync(dto, cancellationToken);
        CategoriesViewModel view = new CategoriesViewModel()
        {
            Categories = categories.Models,
        };
        return View(view);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var category = await _categoriesService.GetAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }

        return Json(category);
    }

    public async Task<IActionResult> Delete([Bind("Id")] int id, CancellationToken cancellationToken)
    {
        await _categoriesService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name")] Category category, CancellationToken cancellationToken)
    {
        await _categoriesService.CreateAsync(category, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id,Name")] Category category, CancellationToken cancellationToken)
    {
        await _categoriesService.UpdateAsync(category, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) 
    {
        var dto = new FilterPaginationDto(string.Empty, 0, 50, "Id",
               SortOrder.Asc);
        var categories = await _categoriesService.GetAllAsync(dto, cancellationToken);

        return Json(categories);
    }
}
