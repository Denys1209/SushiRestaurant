using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.Categories;

public class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

}
