using SushiRestaurant.WebApi.Dtos.Categories;

namespace SushiRestaurant.WebApi.Dtos.CategoryDtos;
public class ReturnCategoryPageDto
{
    public required ICollection<GetCategoryDto> categories { get; set; }
    public required int HowManyPages { get; set; }

}
