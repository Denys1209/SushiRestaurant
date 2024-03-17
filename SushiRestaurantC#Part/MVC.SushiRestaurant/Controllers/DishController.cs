using Microsoft.AspNetCore.Mvc;
using MVC.SushiRestaurant.ViewModels;
using SushiRestaurant.Application.Dishes;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;

namespace MVC.SushiRestaurant.Controllers;

public class DishesController : Controller
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var dto = new FilterPaginationDto(string.Empty, 0, 50, "Id", SortOrder.Asc);
        var dishes = await _dishService.GetAllAsync(dto, cancellationToken);
        DishesViewModel view = new DishesViewModel()
        {
            Dishes = dishes.Models,
        };
        return View(view);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var dish = await _dishService.GetAsync(id, cancellationToken);
        if (dish == null)
        {
            return NotFound();
        }

        return Json(dish);
    }

    public async Task<IActionResult> Delete([Bind("Id")] int id, CancellationToken cancellationToken)
    {
        await _dishService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,Description,Cost,CategoryId")] Dish dish, CancellationToken cancellationToken)
    {
        await _dishService.CreateAsync(dish, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id,Name,Description,Cost,CategoryId")] Dish dish, CancellationToken cancellationToken)
    {
        await _dishService.UpdateAsync(dish, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

}
