
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;

public class DishFoodSetRepository : CrudRepository<DishFoodSet>, IDishFoodSetRespository
{
    public DishFoodSetRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<DishFoodSet> Filter(IQueryable<DishFoodSet> query, string filter)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<DishFoodSet> Sort(IQueryable<DishFoodSet> query, string orderBy, bool isAscending)
    {
        throw new NotImplementedException();
    }

    protected override void Update(DishFoodSet model, DishFoodSet entity)
    {
        entity.Dish = model.Dish;
        entity.DishId = model.DishId;
        entity.FoodSet = model.FoodSet;
        entity.FoodSetId = model.FoodSetId;
    }
}
