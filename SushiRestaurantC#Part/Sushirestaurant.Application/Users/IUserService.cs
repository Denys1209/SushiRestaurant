using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Users;

public interface IUserService : ICrudService<User>
{
    Task CheckUserExistsAsync(string email, CancellationToken cancellationToken);
}

