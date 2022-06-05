using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Revix.Rate.Domain.Models;

public class CoinRate : IEntity {
    public IEnumerable<RateItem> Data { get; set; }
    public Status Status { get; set; }

    [BsonId]
    [BsonRepresentation (BsonType.ObjectId)]
    public string Id { get; set; }
}