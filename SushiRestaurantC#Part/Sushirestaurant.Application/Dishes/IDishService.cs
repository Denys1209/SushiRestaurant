using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Dishes;

public interface IDishService : ICrudService<Dish>
{

    public Task<IReadOnlyCollection<Dish>> getAllDishesByCategoryAsync(Category category, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<Dish>> GetAllDishesByCategoryAsync(string categoryName, CancellationToken cancellationToken);
}
