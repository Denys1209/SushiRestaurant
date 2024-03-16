using System.Text.Json;
using MVC.SushiRestaurant.ApiServices.CollectionConverters;
using SushiRestaurant.Application.FoodSets;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain.Models;
using SushiRstaurant.Domain;

namespace MVC.SushiRestaurant.ApiServices.Services;

public class FoodSetApiService : IFoodSetService
{
    private readonly HttpClient _httpClient;

    public FoodSetApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> CreateAsync(FoodSet model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("FoodSets", model, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"FoodSets/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PaginatedCollection<FoodSet>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        options.Converters.Add(new PaginatedCollectionConverter<FoodSet>());
        var response = await _httpClient.GetFromJsonAsync<PaginatedCollection<FoodSet>>($"FoodSets", options, cancellationToken);
        return response;
    }

   
    public async Task<FoodSet?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<FoodSet>($"FoodSets/{id}", cancellationToken);
        return response;
    }

    public async Task UpdateAsync(FoodSet model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"FoodSets/{model.Id}", model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

     public async Task<IReadOnlyCollection<FoodSet?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("FoodSets/multiple", ids, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<FoodSet?>>(cancellationToken);
    }
}
