using Microsoft.EntityFrameworkCore;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.FoodSets;
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

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IDishService, DishService>();
        services.AddTransient<IFoodSetService, IFoodSetService>();
    }
}
