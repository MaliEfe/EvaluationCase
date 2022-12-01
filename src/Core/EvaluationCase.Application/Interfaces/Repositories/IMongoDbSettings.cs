
namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
