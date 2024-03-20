using System.ComponentModel.DataAnnotations;

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


    //public ICollection<UserDish> UserDishs { get; set; }
    public User(string email, string password, string userName) 
    {
        this.Email = email;
        this.Password = password;
        this.Username = userName;
        //this.UserDishs = new List<UserDish>();
    }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }
}
