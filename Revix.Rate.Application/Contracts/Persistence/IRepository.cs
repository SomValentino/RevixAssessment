using Revix.Rate.Domain.Models;
using MongoDB.Driver;

namespace Revix.Rate.Application.Contracts.Persistence;

public interface IRepository<TEntity> where TEntity : IEntity {
    Task<IReadOnlyList<TEntity>> GetAsync ();
    Task<TEntity> GetAsync (string Id);
    Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<TEntity> Filter);
    Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<TEntity> Filter, int page = 1, int pageSize = 10);

    Task<TEntity> Create (TEntity entity);
    Task<bool> Delete (string Id);
    Task<bool> Update (TEntity entity);

}