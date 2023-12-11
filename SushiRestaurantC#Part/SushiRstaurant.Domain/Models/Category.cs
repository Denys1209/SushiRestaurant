
namespace SushiRstaurant.Domain.Models;

sealed public class Category : Model
{
    public string Name { get; set; }


    public Category(string name) 
    {
        Name = name;
    }


    public override string ToString()
    {
        return $"{Name}";
    }
}
