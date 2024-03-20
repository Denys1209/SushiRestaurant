namespace SushiRstaurant.Domain.Models;
public class UserDish : Model
{

    public int DishId { get; set; }
    public required Dish Dish { get; set; }

    public int UserId { get; set; }
    public required User User { get; set; }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }
}
