using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Dishes;

public interface IDishRepository : ICrudRepository<Dish>
{
    IReadOnlyCollection<Dish> GetAllDishesByCategory(string category, CancellationToken cancellationToken);
}
