﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRstaurant.Domain.Models;
public class Order : Model
{
    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }


    [ForeignKey("UserId")]
    public required User User { get; set; }
    public DateTime DateTime { get; set; }
    public ICollection<OrderDish> OrderDishes { get; set; }

    public Order(decimal cost, User user, DateTime dateTime) 
    {
        this.Cost = cost;
        this.User = user;
        this.DateTime = dateTime;
        this.OrderDishes = new List<OrderDish>();
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
        throw new NotImplementedException();
    }
}
