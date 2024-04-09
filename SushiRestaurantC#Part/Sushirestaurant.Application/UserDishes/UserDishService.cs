
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.UserDishes;
public class UserDishService : CrudService<UserDish>, IUserDishService
{
    public UserDishService(IUserDishRepository repository) : base(repository)
    {

    }

    public async Task<IReadOnlyCollection<Dish>> GetAllFavoriteDishesOfUserAsync(User user, CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAsync(cancellationToken)).Where(e => e.UserId == user.Id).Select(e => e.Dish).ToList();
    }

    public async Task<IReadOnlyCollection<int>> GetAllFavoriteDishesIdOfUserAsync(User user, CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAsync(cancellationToken)).Where(e => e.UserId == user.Id).Select(e => e.Dish.Id).ToList();
    }

    public async Task DeleteFavoriteDishAsync(User user, Dish dish, CancellationToken cancellationToken)
    {
        var userDish = (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault(e => e.UserId == user.Id && e.DishId == dish.Id);
        if (userDish == null) return;
        await _repository.DeleteAsync(userDish.Id, cancellationToken);
    }
}
