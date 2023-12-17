using SushiRestaurant.Constants;

namespace SushiRstaurant.Domain.Models;

public sealed class Category : Model
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

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            Constants.NameStringCategory => Name,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
