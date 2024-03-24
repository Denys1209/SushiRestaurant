using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.WebApi.Dtos.OrderDtos;
public class CreateOrderDto
{
    [Column(TypeName = "money")]
    public required decimal Cost { get; set; }


       public DateTime DateTime { get; set; }


}
