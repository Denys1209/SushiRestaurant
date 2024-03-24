
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.OrderDishes;
public interface IOrderDishService : ICrudService<OrderDish>
{

    public Task<IReadOnlyCollection<OrderDish>> GetAllOrderDishesInOrderIdAsync(int orderId, CancellationToken cancellationToken);
}
