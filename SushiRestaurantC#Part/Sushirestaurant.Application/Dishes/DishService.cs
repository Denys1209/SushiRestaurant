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
        return (await _repository.GetAllAsync(cancellationToken)).Where(e => e.Category == category).ToArray();
    }

    public async Task<IReadOnlyCollection<Dish>> GetAllDishesByCategoryAsync(string categoryName, CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAsync(cancellationToken)).Where(e => e.Category.Name.ToLower() == categoryName.ToLower()).ToArray();
    }

    public async Task<IReadOnlyCollection<Dish>> GetAllDishesInFoodSetIdAsync(int foodSetId, CancellationToken cancellationToken)
    {
        return (await _repository.GetDishFoodSetsAsync(cancellationToken))
              .Where(m => m.FoodSetId == foodSetId)
              .Select(e => e.Dish)
              .ToList();

    }
}
