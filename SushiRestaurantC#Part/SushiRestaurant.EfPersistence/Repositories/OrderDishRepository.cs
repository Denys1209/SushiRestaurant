
using SushiRestaurant.Application.OrderDishes;
using SushiRestaurant.Application.Users;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;
public sealed class OrderDishRepository : CrudRepository<OrderDish>, IOrderDishRepository
{
    public OrderDishRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<OrderDish> Filter(IQueryable<OrderDish> query, string filter)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<OrderDish> Sort(IQueryable<OrderDish> query, string orderBy, bool isAscending)
    {
        throw new NotImplementedException();
    }

    protected override void Update(OrderDish model, OrderDish entity)
    {
        entity.Dish = model.Dish;
        entity.DishId = model.DishId;
        entity.quantity = model.quantity;
        entity.Order = model.Order;
        entity.OrderId = model.OrderId;
    }
}
