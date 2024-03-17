using MVC.SushiRestaurant.ApiServices.CollectionConverters;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;
using System.Text.Json;

namespace MVC.SushiRestaurant.ApiServices.Services;

public class DishApiService : IDishService
{
    private readonly HttpClient _httpClient;

    public DishApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> CreateAsync(Dish model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("Dishes", model, cancellationToken);
        response.EnsureSuccessStatusCode();
        return int.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"Dishes/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PaginatedCollection<Dish>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Converters.Add(new PaginatedCollectionConverter<Dish>());
        var response = await _httpClient.GetFromJsonAsync<PaginatedCollection<Dish>>($"Dishes", options, cancellationToken);
        return response;
    }

    public async Task<IReadOnlyCollection<Dish>> GetAllDishesByCategoryAsync(string categoryName, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"Dishes/{categoryName}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Dish>>(cancellationToken);
    }

    public Task<IReadOnlyCollection<Dish>> getAllDishesByCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Dish>> GetAllDishesInFoodSetIdAsync(int foodSetId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"Dishes/FoodSet/{foodSetId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Dish>>(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Dish?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("Dishes/multiple", ids, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Dish?>>(cancellationToken);
    }

    public async Task<Dish?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"Dishes/{id}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Dish>(cancellationToken);
        }
        else
        {
            return null;
        }
    }

    public async Task UpdateAsync(Dish model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"Dishes/{model.Id}", model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
