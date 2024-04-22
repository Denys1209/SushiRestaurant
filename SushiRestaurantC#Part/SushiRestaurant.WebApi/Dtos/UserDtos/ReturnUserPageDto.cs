namespace SushiRestaurant.WebApi.Dtos.UserDtos;
public class ReturnUserPageDto
{
    public required ICollection<GetUserDto> Users { get; set; }
    public required int HowManyPages { get; set; }
}
