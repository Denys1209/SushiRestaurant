﻿using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.Application.Shared;

public abstract class CrudService<TModel> : ICrudService<TModel> where TModel : Model
{
    protected readonly ICrudRepository<TModel> _repository;

    public CrudService(ICrudRepository<TModel> repository)
    {
        _repository = repository;
    }

    public async virtual Task<int> CreateAsync(TModel model, CancellationToken cancellationToken)
    {
        return await _repository.AddAsync(model, cancellationToken);
    }

    public async virtual Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }

    public async virtual Task<PaginatedCollection<TModel>> GetAllAsync(FilterPaginationDto dto, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(dto, cancellationToken);
    }
    public async virtual Task<IReadOnlyCollection<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async virtual Task<TModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(id, cancellationToken);
    }

    public async virtual Task<IReadOnlyCollection<TModel?>> GetAllModelsByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {

        return await _repository.GetAllModelsByIdsAsync(ids, cancellationToken);
    }


    public async virtual Task UpdateAsync(TModel model, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(model, cancellationToken);
    }

    public async Task<int> GetNumberOfPagesAsync(int sizeOfPage, CancellationToken cancellationToken)
    {
        return await _repository.GetNumberOfPagesAsync(sizeOfPage, cancellationToken);
    }
}
