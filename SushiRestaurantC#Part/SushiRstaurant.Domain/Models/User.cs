using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations;

namespace SushiRstaurant.Domain.Models;

public sealed class User : Model
{


    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Role { get; set; }

    [Required]
    public string VerifyToken { get; set; }

    [Required]
    public bool IsVerify { get; set; }

    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }

    public DateTime? VerifiedAt;


    [Required]
    public DateTime RegistrationDateTime { get; set; }
    public ICollection<UserDish> UserDishes { get; set; }

    public User(string email, string password, string userName, string role, string verifyToken, DateTime registrationDateTime)
    {
        this.Email = email;
        this.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        this.Username = userName;
        this.Role = role;
        this.UserDishes = new List<UserDish>();
        this.VerifyToken = verifyToken;
        this.RegistrationDateTime = registrationDateTime;
        this.IsVerify = false;
    }
    public User() 
    {
        this.UserDishes = new List<UserDish>();
        this.IsVerify = false;
    }
    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            Constants.UserNameStringUser => Username,
            Constants.EmailStringUser => Email,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
