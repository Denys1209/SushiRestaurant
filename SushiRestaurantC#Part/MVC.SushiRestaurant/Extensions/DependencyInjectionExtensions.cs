using MVC.SushiRestaurant.ApiServices;
using MVC.SushiRestaurant.Constants;
using SushiRestaurant.Application.Categories;

namespace MVC.SushiRestaurant.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICategoryService, CategoryApiService>(client =>
        {
            client.BaseAddress = new Uri(WebSitesConstants.ApiSushiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        });
    }
}
