using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.UserDishes;
public interface IUserDishService : ICrudService<UserDish>
{
    public  Task<IReadOnlyCollection<Dish>> GetAllFavoriteDishesOfUserAsync(User user, CancellationToken cancellationToken);
    public  Task<IReadOnlyCollection<int>> GetAllFavoriteDishesIdOfUserAsync(User user, CancellationToken cancellationToken);
    public  Task DeleteFavoriteDishAsync(User user, Dish dish, CancellationToken cancellationToken);
}
