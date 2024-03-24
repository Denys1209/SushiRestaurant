
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Users;

public sealed class UsersService : CrudService<User>, IUserService
{
    public UsersService(IUserRepository repository) : base(repository)
    {
    }

    public async Task<bool> CheckUserExistsAsync(string email, CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault((user) => user.Email == email) != null;
    }

    public async Task<User?> ValidateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault(user => user.Email == email);
        if (user == null)
        {
            return null;
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return user;
        }
        else
        {
            return null;
        }
    }
}
