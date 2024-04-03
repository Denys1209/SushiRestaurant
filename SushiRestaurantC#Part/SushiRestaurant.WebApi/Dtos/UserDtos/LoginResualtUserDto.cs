namespace SushiRestaurant.WebApi.Dtos.UserDtos;
public class LoginResualtUserDto : GetUserDto
{
    public required string Token { get; set; }
    public required ICollection<int> FavoriteDishesIds { get; set;}
}
