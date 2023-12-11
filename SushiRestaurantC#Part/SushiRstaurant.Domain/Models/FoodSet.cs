using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;

class FoodSet : Model
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageURL { get; set; }

    public ICollection<Dish> Dishes { get; set; }

    public Category Category { get; set; }
    
    [Column(TypeName = "money")]
    public decimal Cost { get; set; }

    public FoodSet(string name, string description, decimal cost, Category category,  string imageUrl = Constants.DefaultImageForFood) 
    {
        Name = name;
        Description = description;
        Cost = cost;
        Category = category;
        ImageURL = imageUrl;
        Dishes = new List<Dish>();
    }

}
