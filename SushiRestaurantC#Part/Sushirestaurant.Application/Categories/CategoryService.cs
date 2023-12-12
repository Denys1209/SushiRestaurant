using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Categories;


public sealed class CategoryService : CrudService<Category>, ICategoryService
{
    public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
    }
}