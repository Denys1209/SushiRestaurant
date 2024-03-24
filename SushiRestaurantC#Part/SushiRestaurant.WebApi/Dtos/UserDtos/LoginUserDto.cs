using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.UserDtos;
public class LoginUserDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public required string Email { get; set; }


    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }
}
