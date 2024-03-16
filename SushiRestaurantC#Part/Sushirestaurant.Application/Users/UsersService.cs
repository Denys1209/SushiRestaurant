
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Users;

public sealed class UsersService : CrudService<User>, IUserService
{
    public UsersService(IUserRepository repository) : base(repository)
    {
    }

    public async Task CheckUserExistsAsync(string email, CancellationToken cancellationToken)
    {
        (await _repository.GetAllAsync(cancellationToken))
    }
}
