using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.FoodSets;

public class UpdateFoodSetDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(256, MinimumLength = 3)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "ImageURL is required")]
    public string ImageURL { get; set; } = null!;


    [Required(ErrorMessage = "Dishes is required")]
    public ICollection<Dish> Dishes { get; set; } = null!;

    [Required(ErrorMessage = "Category is required")]
    public Category Category { get; set; } = null!;

    [Required(ErrorMessage = "Cost is required")]
    [Column(TypeName = "money")]
    public decimal Cost { get; set; }
}
