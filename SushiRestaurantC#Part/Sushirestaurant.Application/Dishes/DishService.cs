using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Dishes;

public class DishService : CrudService<Dish>, IDishService
{
    public DishService(IDishRepository dishRepository) : base(dishRepository)
    {
    }
}
