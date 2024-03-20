
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Orders;
public class OrderService : CrudService<Order>, IOrderService
{
    public OrderService(IOrderRepository repository) : base(repository)
    {
    }
}
