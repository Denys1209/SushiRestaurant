using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.Application.Users;
using SushiRestaurant.EfPersistence.Data;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Repositories;

public sealed class UserRepository : CrudRepository<User>, IUserRepository
{
    public UserRepository(SushiRestaurantDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<User> Filter(IQueryable<User> query, string filter)
    {
        return query.Where(m => m.Email.Contains(filter));
    }

    protected override IQueryable<User> Sort(IQueryable<User> query, string orderBy, bool isAscending)
    {
        return orderBy switch
        {
            Constants.Constants.EmailStringUser => isAscending ? query.OrderBy(m => m.Email) : query.OrderByDescending(m => m.Email),
            Constants.Constants.UserNameStringUser => isAscending ? query.OrderBy(m => m.Username) : query.OrderByDescending(m => m.Username),
            Constants.Constants.IdStringName => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            _ => isAscending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id)
        };
    }

    protected override void Update(User model, User entity)
    {
        entity.Email = model.Email;
        entity.Username = model.Username;
        entity.PasswordHash = model.PasswordHash;
        entity.IsVerify = model.IsVerify;
        entity.Role = model.Role;
    }
}

