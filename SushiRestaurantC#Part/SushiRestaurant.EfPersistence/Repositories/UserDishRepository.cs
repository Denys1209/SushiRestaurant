using Microsoft.EntityFrameworkCore;
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.UserDishes;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;
public class UserDishRepository : CrudRepository<UserDish>, IUserDishRepository
{
    public UserDishRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }

        protected override IQueryable<UserDish> Filter(IQueryable<UserDish> query, string filter)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<UserDish> Sort(IQueryable<UserDish> query, string orderBy, bool isAscending)
    {
        throw new NotImplementedException();
    }

    protected override void Update(UserDish model, UserDish entity)
    {
        entity.Dish = model.Dish;
        entity.DishId = model.DishId;
        entity.User = model.User;
        entity.UserId = model.UserId;
    }
}