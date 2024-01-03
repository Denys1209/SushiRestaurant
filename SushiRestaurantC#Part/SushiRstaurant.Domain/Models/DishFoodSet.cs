
namespace SushiRstaurant.Domain.Models;

public sealed class DishFoodSet : Model
{
    public int DishId { get; set; }
    public required Dish Dish { get; set; }

    public int FoodSetId { get; set; }
    public required FoodSet FoodSet { get; set; }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }
}
