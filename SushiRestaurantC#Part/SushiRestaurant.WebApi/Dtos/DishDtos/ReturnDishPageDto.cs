using SushiRestaurant.WebApi.Dtos.Dish;

namespace SushiRestaurant.WebApi.Dtos.DishDtos;
public class ReturnDishPageDto
{
    public required ICollection<GetDishDto> Dishes { get; set; }
    public required int HowManyPages { get; set; }

}
