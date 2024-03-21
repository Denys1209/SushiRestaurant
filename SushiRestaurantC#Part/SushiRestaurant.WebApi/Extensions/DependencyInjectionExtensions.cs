using Microsoft.EntityFrameworkCore;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.DIshesFoodSets;
using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.Application.OrderDishes;
using SushiRestaurant.Application.Orders;
using SushiRestaurant.Application.Shared;
using SushiRestaurant.Application.UserDishes;
using SushiRestaurant.Application.Users;
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
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDishRepository, OrderDishRepository>();
        services.AddTransient<IUserDishRepository, UserDishRepository>();

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IDishService, DishService>();
        services.AddTransient<IFoodSetService, FoodSetService>();
        services.AddTransient<IDishFoodSetService, DishesFoodSetService>();
        services.AddTransient<IUserService, UsersService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IOrderDishService, OrderDishService>();
        services.AddTransient<IUserDishService, UserDishService>();
        
    }
}
