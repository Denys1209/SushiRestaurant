using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SushiRstaurant.Domain.Models;

public sealed class User : Model
{
    

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; }

    public ICollection<UserDish> UserDishes { get; set; }
    public User(string email, string password, string userName) 
    {
        this.Email = email;
        this.Password = password;
        this.Username = userName;
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
