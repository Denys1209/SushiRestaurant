using Microsoft.EntityFrameworkCore;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.EfPersistence.Data;

public class SushiRestaurantDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<FoodSet> FoodSets { get; set; }

    public SushiRestaurantDbContext(DbContextOptions<SushiRestaurantDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
