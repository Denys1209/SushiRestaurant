using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.FoodSets;

public interface IFoodRepository : ICrudRepository<FoodSet>
{
}
