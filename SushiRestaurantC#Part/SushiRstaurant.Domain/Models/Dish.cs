﻿using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;


public sealed class Dish : Model
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageURL { get; set; }

    public ICollection<DishFoodSet> DishFoodSets { get; set; }


    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }

    public Dish(string name, string description, decimal cost, Category category, string imageUrl = Constants.DefaultImageForFood) 
    {
        Name = name;
        Description = description;
        Cost = cost;
        ImageURL = imageUrl;
        Category = category;
        DishFoodSets = new List<DishFoodSet>();
    }

    public Dish() {
        DishFoodSets = new List<DishFoodSet>();
    }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            Constants.NameStringDish => Name,
            Constants.DescriptionStringDish => Description,
            Constants.ImageURLStringDish => ImageURL,
            Constants.CostStringDish => Cost,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
