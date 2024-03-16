using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MVC.SushiRestaurant.ApiServices.Services;
using MVC.SushiRestaurant.Constants;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Dishes;
using System.Text;

namespace MVC.SushiRestaurant.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICategoryService, CategoryApiService>(client =>
        {
            client.BaseAddress = new Uri(WebSitesConstants.ApiSushiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.sushiCategory.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-SushiCategory");
        });
        
        services.AddHttpClient<IDishService, DishApiService>(client =>
        {
            client.BaseAddress = new Uri(WebSitesConstants.ApiSushiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.sushiCategory.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-SushiCategory");
        });


       
    }
}
