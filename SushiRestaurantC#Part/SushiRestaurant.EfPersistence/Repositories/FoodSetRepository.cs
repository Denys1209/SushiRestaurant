using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;

public sealed class FoodSetRepository : CrudRepository<FoodSet>, IFoodRepository
{
    public FoodSetRepository(SushiRestaurantDbContext dbContext) : base(dbContext) { }


    protected override IQueryable<FoodSet> Filter(IQueryable<FoodSet> query, string filter)
    {
        return query.Where(m => m.Name.Contains(filter));
    }

    protected override IQueryable<FoodSet> Sort(IQueryable<FoodSet> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            Constants.Constants.NameStringFoodSet => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            Constants.Constants.CostStringFoodSet => isAscending ? query.OrderBy(m => m.Cost) : query.OrderByDescending(m => m.Cost),
            Constants.Constants.DescriptionStringFoodSet => isAscending ? query.OrderBy(m => m.Description) : query.OrderByDescending(m => m.Description),
            Constants.Constants.IdStringName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(FoodSet model, FoodSet entity)
    {
        entity.Name = model.Name;
        entity.Cost = model.Cost;
        entity.Description = model.Description;
    }
}
