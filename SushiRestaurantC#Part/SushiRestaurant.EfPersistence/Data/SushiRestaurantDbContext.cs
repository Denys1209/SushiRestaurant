using Microsoft.EntityFrameworkCore;
using SushiRstaurant.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiRestaurant.EfPersistence.Data;

public class SushiRestaurantDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<FoodSet> FoodSets { get; set; }
    public DbSet<DishFoodSet> DishFoodSets { get; set; }


    public SushiRestaurantDbContext(DbContextOptions<SushiRestaurantDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishFoodSet>()
                     .HasKey(pc => new { pc.DishId, pc.FoodSetId });
        modelBuilder.Entity<DishFoodSet>()
                  .HasOne(p => p.FoodSet)
                  .WithMany(pc => pc.DishFoodSets)
                  .HasForeignKey(p => p.FoodSetId);
        modelBuilder.Entity<DishFoodSet>()
                .HasOne(p => p.Dish)
                .WithMany(pc => pc.DishFoodSets)
                .HasForeignKey(c => c.DishId);

    }
}
