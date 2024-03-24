using AutoMapper;
using SushiRestaurant.WebApi.Dtos.Categories;
using SushiRestaurant.WebApi.Dtos.Dish;
using SushiRestaurant.WebApi.Dtos.FoodSet;
using SushiRestaurant.WebApi.Dtos.FoodSets;
using SushiRestaurant.WebApi.Dtos.OrderDishDtos;
using SushiRestaurant.WebApi.Dtos.OrderDtos;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<Dish, UpdateDishDto>();
            CreateMap<FoodSet, UpdateFoodSetDto>();
            CreateMap<User,  UpdateUserDto>();
            CreateMap<Order,  UpdateOrderDto>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<Dish, GetDishDto>();
            CreateMap<FoodSet, GetFoodSetDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Order,  GetOrderDto>();
            CreateMap<OrderDish,  GetOrderDishDto>();

            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateDishDto, Dish>();
            CreateMap<UpdateFoodSetDto,FoodSet>();
            CreateMap<UpdateUserDto,User>();
            CreateMap<UpdateOrderDto,Order>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateDishDto, Dish>();
            CreateMap<CreateFoodSetDto,FoodSet>();
            CreateMap<CreateUserDto,User>();
            CreateMap<CreateOrderDto,Order>();






        }
    }
}
