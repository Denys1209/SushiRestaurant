
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;

public sealed class DishRepository : CrudRepository<Dish>, IDishRepository
{
    public DishRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {

    }

    protected override IQueryable<Dish> Filter(IQueryable<Dish> query, string filter)
    {
        return query.Where(m => m.Name.Contains(filter));
    }

    protected override IQueryable<Dish> Sort(IQueryable<Dish> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            Constants.Constants.NameStringDish => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            Constants.Constants.CostStringDish => isAscending ? query.OrderBy(m => m.Cost) : query.OrderByDescending(m => m.Cost),
            Constants.Constants.DescriptionStringDish => isAscending ? query.OrderBy(m => m.Description) : query.OrderByDescending(m => m.Description),
            Constants.Constants.IdStringName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(Dish model, Dish entity)
    {
        entity.Name = model.Name;
        entity.Cost = model.Cost;
        entity.Description = model.Description;
    }
}
