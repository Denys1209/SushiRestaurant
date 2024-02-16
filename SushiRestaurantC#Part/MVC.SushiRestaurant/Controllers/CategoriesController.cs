using Microsoft.AspNetCore.Mvc;
using MVC.SushiRestaurant.ViewModels;
using SushiRestaurant.Application.Categories;
using SushiRstaurant.Domain;


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
        var dto = new FilterPaginationDto(string.Empty, 0, 50, "id",
                SortOrder.Asc);

        var categories = await _categoriesService.GetAllAsync(dto, cancellationToken);
        CategoriesViewModel view = new CategoriesViewModel()
        {
            Categories = categories.Models,
        };
        return View(view);
    }
}
