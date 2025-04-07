using MongoDB.Driver;

namespace ProcessadorTarefas.Infrastructure.Persistence
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}