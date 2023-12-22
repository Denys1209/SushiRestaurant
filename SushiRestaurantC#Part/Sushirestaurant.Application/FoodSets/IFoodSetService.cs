using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.FoodSets;

public interface IFoodSetService : ICrudService<FoodSet>
{
    public async Task<PaginatedCollection<Dish>> GetAllByCategory(FilterPaginationDto dto, CancellationToken cancellationToken) 
    {
        throw new NotImplementedException(); 
    }
}
