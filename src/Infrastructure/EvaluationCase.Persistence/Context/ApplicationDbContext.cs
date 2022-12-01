using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Persistence.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EvaluationCase.Persistence.Context
{
    public class ApplicationDbContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }

        IMongoDatabase IMongoDBContext._db { get { return _db; } }

        MongoClient IMongoDBContext._mongoClient { get { return _mongoClient; } }

        public ApplicationDbContext(IOptions<DatabaseSetting> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
