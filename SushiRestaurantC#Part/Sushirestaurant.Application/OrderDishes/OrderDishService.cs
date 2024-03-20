
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.OrderDishes;
public class OrderDishService : CrudService<OrderDish>, IOrderDishService
{
    public OrderDishService(IOrderDishRepository repository) : base(repository)
    {
    }
}
