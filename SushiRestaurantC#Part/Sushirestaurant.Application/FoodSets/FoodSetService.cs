using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.FoodSets;

public class FoodSetService : CrudService<FoodSet>,IFoodSetService
{
    public FoodSetService(IFoodRepository foodSetRepository) : base(foodSetRepository)
    {
    }
}
