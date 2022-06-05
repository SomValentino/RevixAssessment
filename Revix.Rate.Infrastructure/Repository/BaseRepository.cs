using MongoDB.Driver;
using Revix.Rate.Application.Contracts;
using Revix.Rate.Application.Contracts.Persistence;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Infrastructure.Repository;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity {
    protected readonly IDataContext<TEntity> _context;

    public BaseRepository (IDataContext<TEntity> context) {
        _context = context;
    }
    public async Task<TEntity> Create (TEntity entity) {
        await _context.Collections.InsertOneAsync (entity);

        return entity;
    }

    public async Task<bool> Delete (string Id) {
        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, Id);

        var result = await _context.Collections.DeleteOneAsync (filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync () {
        return await _context.Collections.Find (p => true).ToListAsync ();
    }

    public async Task<TEntity> GetAsync (string Id) {
        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, Id);

        return await _context.Collections.Find (filter).FirstOrDefaultAsync ();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<TEntity> filter) {
        var data = await _context.Collections.Find (filter).ToListAsync ();

        return data;
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync (FilterDefinition<TEntity> filter, int page = 1, int pageSize = 10) {
        var data = await _context.Collections.Find (filter).ToListAsync ();

        return data.Skip (pageSize * (page - 1)).Take (pageSize).ToList ();
    }

    public async Task<bool> Update (TEntity entity) {

        var filter = Builders<TEntity>.Filter.Eq (x => x.Id, entity.Id);

        var result = await _context.Collections.ReplaceOneAsync (filter, entity);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}