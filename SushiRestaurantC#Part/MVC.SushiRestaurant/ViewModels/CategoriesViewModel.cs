using SushiRstaurant.Domain.Models;

namespace MVC.SushiRestaurant.ViewModels;

sealed public class CategoriesViewModel
{
    public IReadOnlyCollection<Category> Categories { get; set; } = null!;

}
