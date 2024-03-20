
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.UserDishes;
public class UserDishService : CrudService<UserDish>, IUserDishService
{
     public UserDishService(IUserDishRepository repository) : base(repository)
     {

     }

}
