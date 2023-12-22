using Microsoft.AspNetCore.Mvc.Filters;
using SushiRestaurant.WebApi.Filters.Validation;

namespace SushiRestaurant.WebApi.Extensions;

public static class ValidationFilterExtensions
{
    public static FilterCollection AddValidationFilter(this FilterCollection filters)
    {
        filters.Add<ValidationFeatureFilter>();
        return filters;
    }
}
