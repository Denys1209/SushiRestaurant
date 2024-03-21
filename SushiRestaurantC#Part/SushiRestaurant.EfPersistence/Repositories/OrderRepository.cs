using SushiRestaurant.Application.Orders;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;
public sealed class OrderRepository : CrudRepository<Order>, IOrderRepository
{
    public OrderRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }


    protected override IQueryable<Order> Filter(IQueryable<Order> query, string filter)
    {
        throw new NotImplementedException();
    }


    protected override IQueryable<Order> Sort(IQueryable<Order> query, string orderBy, bool isAscending)
    {
        throw new NotImplementedException();
    }


    protected override void Update(Order model, Order entity)
    {
        entity.User = model.User;
        entity.OrderDishes = model.OrderDishes;
        entity.Cost = model.Cost;
        entity.DateTime = model.DateTime;
    }
}
