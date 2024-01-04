using AutoMapper;
using SushiRestaurant.WebApi.Dtos.Categories;
using SushiRestaurant.WebApi.Dtos.Dish;
using SushiRestaurant.WebApi.Dtos.FoodSet;
using SushiRestaurant.WebApi.Dtos.FoodSets;
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

            CreateMap<Category, GetCategoryDto>();
            CreateMap<Dish, GetDishDto>();
            CreateMap<FoodSet, GetFoodSetDto>();



            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<UpdateDishDto, Dish>();
            CreateMap<UpdateFoodSetDto,FoodSet>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateDishDto, Dish>();
            CreateMap<CreateFoodSetDto,FoodSet>();


        }
    }
}
