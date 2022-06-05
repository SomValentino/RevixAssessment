using MongoDB.Driver;
using Revix.Rate.Application.Contracts;
using Revix.Rate.Domain.Models;

namespace Revix.Rate.Infrastructure.Data;

public class RateDataContext : IDataContext<ApiResponse> {
    public RateDataContext (string connectionString, string database, string collectionName) 
    {
        Client = new MongoClient (connectionString);
        Database = Client.GetDatabase (database);
        Collections = Database.GetCollection<ApiResponse> (collectionName);
    }
    public IMongoCollection<ApiResponse> Collections { get; set; }
    public IMongoClient Client { get; set; }
    public IMongoDatabase Database { get; set; }
}