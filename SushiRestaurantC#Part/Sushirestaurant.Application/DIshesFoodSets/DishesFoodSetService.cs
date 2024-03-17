
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.DIshesFoodSets;

public sealed class DishesFoodSetService : CrudService<DishFoodSet>, IDishFoodSetService
{
    public DishesFoodSetService(IDishFoodSetRespository repository) : base(repository)
    {
    }
}
