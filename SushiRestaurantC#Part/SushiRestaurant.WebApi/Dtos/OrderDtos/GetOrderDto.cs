using SushiRestaurant.WebApi.Dtos.OrderDishDtos;
using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.WebApi.Dtos.OrderDtos;
public class GetOrderDto
{
    public int Id { get; set; }

    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }


    [ForeignKey("UserId")]
    public required User? User { get; set; }
    public DateTime DateTime { get; set; }
    public required string PhoneNumber { get; set; }

    public required ICollection<GetOrderDishDto> OrderDishDtos { get; set; }
}
