﻿using System.ComponentModel.DataAnnotations;

namespace SushiRestaurant.WebApi.Dtos.Dish;

public class UpdateDishDto
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
    public decimal Cost { get; set; }

    public int CategoryId { get; set; }
}
