using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiRstaurant.Domain.Models;

public sealed class User : Model
{
    public User(string email, string password, string userName) 
    {
        this.Email = email;
        this.Password = password;
        this.Username = userName;
    }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }
}
