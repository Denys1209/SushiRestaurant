
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.OrderDishes;
public interface IOrderDishRepository : ICrudRepository<OrderDish>
{
}
