using Microsoft.EntityFrameworkCore;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.Application.Shared;
using SushiRestaurant.EfPersistence.Data;
using SushiRestaurant.EfPersistence.Repositories;

namespace SushiRestaurant.WebApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<SushiRestaurantDbContext>(options =>
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly("SushiRestaurant.EfPersistence")));

        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IDishRepository, DishRepository>();
        services.AddTransient<IFoodSetRepository, FoodSetRepository>();
        services.AddTransient<IDishFoodSetRespository, DishFoodSetRepository>();

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IDishService, DishService>();
        services.AddTransient<IFoodSetService, FoodSetService>();
        services.AddTransient<IDishFoodSetService, DishesFoodSetService>();
    }
}
