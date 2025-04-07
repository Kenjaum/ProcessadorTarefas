using MongoDB.Driver;

namespace ProcessadorTarefas.Worker.Services.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}