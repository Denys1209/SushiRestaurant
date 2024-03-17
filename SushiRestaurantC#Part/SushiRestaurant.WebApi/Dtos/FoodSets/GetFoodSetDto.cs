using SushiRestaurant.WebApi.Dtos.Categories;
using SushiRestaurant.WebApi.Dtos.Dish;
using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.FoodSets
{
    public class GetFoodSetDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public required string ImageURL { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        public required decimal Cost { get; set; }


        public required ICollection<GetDishDto> Dishes { get; set; }

        public required Category Category { get; set; }
    }
}
