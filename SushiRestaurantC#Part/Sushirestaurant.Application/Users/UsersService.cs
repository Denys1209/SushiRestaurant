
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;
using System.Security.Cryptography;

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

    public async Task<User?> VerifyUser(string email, string token, CancellationToken cancellationToken)
    {
        var user = (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault(user => user.Email == email);
        if (user == null)
        {
            return null;
        }

        if (user.VerifyToken == token)
        {
            user.IsVerify = true;
            user.VerifiedAt = DateTime.Now;
            return user;
        }
        else
        {
            return null;
        }
    }

    public async Task<User?> ForgetPassword(string email, CancellationToken cancellationToken)
    {
        var user = (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault(user => user.Email == email);
        if (user == null)
        {
            return null;
        }

        user.PasswordResetToken = CreateRandomToken();
        user.ResetTokenExpires = DateTime.Now.AddDays(1);
        return user;
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<User?> ResetPassword(string email, string newPassword, string token, CancellationToken cancellationToken)
    {
        var user = (await _repository.GetAllAsync(cancellationToken)).FirstOrDefault(user => user.Email == email);
        if (user == null || user.ResetTokenExpires < DateTime.Now)
        {
            return null;
        }
        if (user.PasswordResetToken == token)
        {

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;
            return user;
        }
        else 
        {
            return null;
        }
    }
}
