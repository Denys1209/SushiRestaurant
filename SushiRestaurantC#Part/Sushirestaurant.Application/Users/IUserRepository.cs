
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Users;

public interface IUserRepository : ICrudRepository<User>
{
}
