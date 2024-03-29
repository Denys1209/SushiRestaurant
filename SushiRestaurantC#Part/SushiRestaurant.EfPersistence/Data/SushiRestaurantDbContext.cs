﻿using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<UserDish>()
                          .HasKey(pc => new { pc.DishId, pc.UserId });

        modelBuilder.Entity<OrderDish>()
                          .HasKey(pc => new { pc.DishId, pc.OrderId });



        modelBuilder.Entity<DishFoodSet>()
                  .HasOne(p => p.FoodSet)
                  .WithMany(pc => pc.DishFoodSets)
                  .HasForeignKey(p => p.FoodSetId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DishFoodSet>()
                .HasOne(p => p.Dish)
                .WithMany(pc => pc.DishFoodSets)
                .HasForeignKey(c => c.DishId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Dish>().Navigation(e => e.Category).AutoInclude();
        modelBuilder.Entity<FoodSet>().Navigation(e => e.Category).AutoInclude();
        modelBuilder.Entity<FoodSet>().Navigation(e => e.DishFoodSets).AutoInclude();
        modelBuilder.Entity<User>().Navigation(e => e.UserDishes).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(e => e.OrderDishes).AutoInclude();
    }
}
