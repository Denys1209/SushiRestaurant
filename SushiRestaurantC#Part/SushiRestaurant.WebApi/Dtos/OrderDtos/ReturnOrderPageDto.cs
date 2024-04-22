namespace SushiRestaurant.WebApi.Dtos.OrderDtos;
public class ReturnOrderPageDto
{
    public required ICollection<GetOrderDto> Orders { get; set; }
    public required int HowManyPages { get; set; }
}
