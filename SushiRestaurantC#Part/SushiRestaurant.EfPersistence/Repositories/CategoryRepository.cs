using SushiRestaurant.Application.Categories;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;


public sealed class CategoryRepository : CrudRepository<Category>, ICategoryRepository
{

    public CategoryRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Category> Sort(IQueryable<Category> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            Constants.Constants.NameStringCategory => isAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name),
            Constants.Constants.IdStringName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override IQueryable<Category> Filter(IQueryable<Category> query, string filter)
    {
        return query.Where(m => m.Name.Contains(filter));
    }
    protected override void Update(Category model, Category entity)
    {
        entity.Name = model.Name;
    }
}