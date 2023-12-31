using AutoMapper;
using SushiRestaurant.WebApi.Dtos;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Dish, DishDto>();
            CreateMap<FoodSet, FoodSetDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<DishDto, Dish>();
            CreateMap<FoodSetDto,FoodSet>();

        }
    }
}
