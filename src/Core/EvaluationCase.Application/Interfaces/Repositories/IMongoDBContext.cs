using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface IMongoDBContext
    {
        IMongoDatabase _db { get; }
        MongoClient _mongoClient { get; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
