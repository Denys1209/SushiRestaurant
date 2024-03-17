using SushiRstaurant.Domain.Models;

namespace MVC.SushiRestaurant.ViewModels;

public class DishesViewModel
{
    public IReadOnlyCollection<Dish> Dishes { get; set; } = null!;

}
