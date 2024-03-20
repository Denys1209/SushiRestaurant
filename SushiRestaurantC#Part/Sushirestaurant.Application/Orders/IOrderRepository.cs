
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Orders;
public interface IOrderRepository : ICrudRepository<Order>
{
}
