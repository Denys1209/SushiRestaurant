
namespace SushiRstaurant.Domain.Models;
public class OrderDish : Model
{
    public int DishId { get; set; }
    public required Dish Dish { get; set; }

    public int OrderId { get; set; }
    public required Order Order { get; set; }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }

}
