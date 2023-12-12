using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Categories;

public interface ICategoryRepository : ICrudRepository<Category>
{
}
