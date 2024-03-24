
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.OrderDishes;
public class OrderDishService : CrudService<OrderDish>, IOrderDishService
{
    public OrderDishService(IOrderDishRepository repository) : base(repository)
    {
    }
    public async Task<IReadOnlyCollection<OrderDish>> GetAllOrderDishesInOrderIdAsync(int orderId, CancellationToken cancellationToken)
    {
         return (await _repository.GetOrderDishsAsync(cancellationToken))
              .Where(m => m.OrderId == orderId)
              .ToList();
    }


}
