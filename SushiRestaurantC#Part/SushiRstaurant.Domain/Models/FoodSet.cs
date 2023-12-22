using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;

public sealed class FoodSet : Model
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageURL { get; set; }

    public required ICollection<Dish> Dishes { get; set; }

    public required Category Category { get; set; }
    
    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }

    public FoodSet(string name, string description, decimal cost, Category category,  string imageUrl = Constants.DefaultImageForFood) 
    {
        Name = name;
        Description = description;
        Cost = cost;
        Category = category;
        ImageURL = imageUrl;
        Dishes = new List<Dish>();
    }

    public FoodSet() { }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            Constants.NameStringFoodSet => Name,
            Constants.DescriptionStringFoodSet => Description,
            Constants.ImageURLStringFoodSet => ImageURL,
            Constants.CostStringFoodSet => Cost,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
