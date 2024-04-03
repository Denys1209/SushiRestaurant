
using SushiRestaurant.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;
public class Order : Model
{
    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }


    [ForeignKey("UserId")]
    public required User? User { get; set; }
    public DateTime DateTime { get; set; }

    public required string PhoneNumber { get; set; }
    public ICollection<OrderDish> OrderDishes { get; set; }

    public Order(decimal cost, DateTime dateTime,string phoneNumber, User? user)
    {
        this.Cost = cost;
        this.User = user;
        this.DateTime = dateTime;
        this.OrderDishes = new List<OrderDish>();
        this.PhoneNumber = phoneNumber;
    }

    public Order() 
    {
        this.OrderDishes = new List<OrderDish>();
    }

    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        return sortColumn switch
        {
            Constants.CostStringOrder => Cost,
            Constants.DateTimeStringOrder => DateTime,
            Constants.IdStringName => Id,
            _ => Id
        };
    }
}
