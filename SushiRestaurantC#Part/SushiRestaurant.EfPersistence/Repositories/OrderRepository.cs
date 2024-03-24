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
        return orderBy switch
        {
            Constants.Constants.DateTimeStringOrder => isAscending ? query.OrderBy(m => m.DateTime) : query.OrderByDescending(m => m.DateTime),
            Constants.Constants.CostStringOrder => isAscending ? query.OrderBy(m => m.Cost) : query.OrderByDescending(m => m.Cost),
            Constants.Constants.IdStringName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }


    protected override void Update(Order model, Order entity)
    {
        entity.User = model.User;
        entity.OrderDishes = model.OrderDishes;
        entity.Cost = model.Cost;
        entity.DateTime = model.DateTime;
    }
}
