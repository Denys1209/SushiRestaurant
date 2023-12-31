﻿using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Dishes;

public interface IDishService : ICrudService<Dish>
{
    IReadOnlyCollection<Dish> GetAllDishesByCategory(string category, CancellationToken cancellationToken);

}
