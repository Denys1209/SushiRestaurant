using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SushiRestaurant.WebApi.Filters.Validation;

public record ValidationFeature(ModelStateDictionary ModelState);
