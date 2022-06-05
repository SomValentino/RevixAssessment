using MongoDB.Driver;
using Revix.Rate.Application.Contracts;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Infrastructure.Data;

public class RateDataContext : IDataContext<CoinRate> {
    public RateDataContext (string connectionString, string database, string collectionName) 
    {
        Client = new MongoClient (connectionString);
        Database = Client.GetDatabase (database);
        Collections = Database.GetCollection<CoinRate> (collectionName);
    }
    public IMongoCollection<CoinRate> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}