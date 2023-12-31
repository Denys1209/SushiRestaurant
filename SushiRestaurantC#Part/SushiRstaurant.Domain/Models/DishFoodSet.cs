
namespace SushiRstaurant.Domain.Models;

public class DishFoodSet
{
    public required int DishId { get; set; }
    public required Dish Dish { get; set; }

    public required int FoodSetId { get; set; }
    public required FoodSet FoodSet { get; set; }
}
