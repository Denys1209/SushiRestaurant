﻿using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.FoodSets;

public sealed class FoodSetService : CrudService<FoodSet>, IFoodSetService
{
    public FoodSetService(IFoodSetRepository foodSetRepository) : base(foodSetRepository)
    {

    }

   
}
