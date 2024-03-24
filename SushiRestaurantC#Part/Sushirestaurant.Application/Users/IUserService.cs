using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Users;

public interface IUserService : ICrudService<User>
{
    Task<bool> CheckUserExistsAsync(string email, CancellationToken cancellationToken);
    Task<User?> ValidateUserAsync(string email, string password, CancellationToken cancellationToken);
}

