using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Dishes;

public class DishService : CrudService<Dish>, IDishService
{
    public DishService(IDishRepository dishRepository) : base(dishRepository)
    {
    }

    public async Task<IReadOnlyCollection<Dish>> getAllDishesByCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        return (IReadOnlyCollection<Dish>)(await _repository.GetAllAsync(cancellationToken)).Where(e => e.Category == category);
    }

    public async Task<IReadOnlyCollection<Dish>> GetAllDishesByCategoryAsync(string categoryName, CancellationToken cancellationToken)
    {
        return (IReadOnlyCollection<Dish>)(await _repository.GetAllAsync(cancellationToken)).Where(e => e.Category.Name == categoryName);
    }
}
