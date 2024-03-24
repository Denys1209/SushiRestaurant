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

    public ICollection<UserDish> UserDishes { get; set; }

    public User(string email, string password, string userName, string role)
    {
        this.Email = email;
        this.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        this.Username = userName;
        this.Role = role;
        this.UserDishes = new List<UserDish>();
    }
    public User() 
    {
        this.UserDishes = new List<UserDish>();
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
