using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.WebApi.Dtos.OrderDtos;
public class UpdateOrderDto
{
    public int Id { get; set; }

    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }


    [ForeignKey("UserId")]
    public required User? User { get; set; }

    public required string PhoneNumber;
    public DateTime DateTime { get; set; }
}
