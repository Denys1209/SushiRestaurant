
using SushiRestaurant.WebApi.Dtos.Dish;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.WebApi.Dtos.OrderDishDtos;
public class GetOrderDishDto
{
    public required GetDishDto Dish { get; set; }
    public uint quantity { get; set; }
}
