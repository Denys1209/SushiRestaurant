using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Shared;

public interface ICrudRepository<TModel> where TModel : Model
{
    Task<int> AddAsync(TModel model, CancellationToken cancellationToken);
    Task UpdateAsync(TModel model, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<TModel?> GetAsync(int id, CancellationToken cancellationToken);
    Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<DishFoodSet>> GetDishFoodSetsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrderDish>> GetOrderDishsAsync(CancellationToken cancellationToken);
    Task<int> GetNumberOfPagesAsync(int sizeOfPage, CancellationToken cancellationToken);

}
