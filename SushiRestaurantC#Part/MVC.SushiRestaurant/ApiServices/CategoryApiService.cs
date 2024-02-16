using SushiRestaurant.Application.Categories;
using SushiRestaurant.Application.Shared;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace MVC.SushiRestaurant.ApiServices;

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
        var response = await _httpClient.GetFromJsonAsync<PaginatedCollection<Category>>($"Categories?page={dto.PageNumber}&size={dto.PageSize}", cancellationToken);
        return response;
    }

    public async Task<IReadOnlyCollection<Category?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("Categories/ids", ids, cancellationToken);
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
        var response = await _httpClient.PutAsJsonAsync($"Categories/{model.Id}", model, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
