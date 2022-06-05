using MongoDB.Driver;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Application.Contracts;

public interface IDataContext<TEntity> where TEntity : IEntity 
{
    public IMongoCollection<TEntity> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}