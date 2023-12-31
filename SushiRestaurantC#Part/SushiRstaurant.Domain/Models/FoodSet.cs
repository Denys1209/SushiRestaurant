using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;

public sealed class FoodSet : Model
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageURL { get; set; }

    public ICollection<DishFoodSet> DishFoodSets { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }

    public FoodSet(string name, string description, decimal cost, Category category,  string imageUrl = Constants.DefaultImageForFood) 
    {
        Name = name;
        Description = description;
        Cost = cost;
        Category = category;
        ImageURL = imageUrl;
        DishFoodSets = new List<DishFoodSet>();
    }

    public FoodSet() {
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
            Constants.NameStringFoodSet => Name,
            Constants.DescriptionStringFoodSet => Description,
            Constants.ImageURLStringFoodSet => ImageURL,
            Constants.CostStringFoodSet => Cost,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
