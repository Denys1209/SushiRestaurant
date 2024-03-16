using MVC.SushiRestaurant.ApiServices.CollectionConverters;
using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;
using System.Text.Json;

namespace MVC.SushiRestaurant.ApiServices.Services;

public class CategoryApiService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> CreateAsync(Category model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("Categories", model, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"Categories/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PaginatedCollection<Category>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Converters.Add(new PaginatedCollectionConverter<Category>());
        var response = await _httpClient.GetFromJsonAsync<PaginatedCollection<Category>>($"Categories", options, cancellationToken);
        return response;
    }

    public async Task<IReadOnlyCollection<Category?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("Categories/multiple", ids, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Category?>>(cancellationToken);
    }


    public async Task<Category?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<Category>($"Categories/{id}", cancellationToken);
        return response;
    }

    public async Task UpdateAsync(Category model, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"Categories", model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
