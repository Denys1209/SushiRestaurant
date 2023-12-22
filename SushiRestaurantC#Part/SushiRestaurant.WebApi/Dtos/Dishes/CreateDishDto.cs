using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.WebApi.Dtos.Dishes;

public class CreateDishDto
{

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "ImageURL is required")]
    public string ImageURL { get; set; } = null!;

    [Required(ErrorMessage = "Category is required")]
    public Category Category { get; set; } = null!;

    [Required(ErrorMessage = "Cost is required")]
    [Column(TypeName = "money")]
    public decimal Cost { get; set; }
}
