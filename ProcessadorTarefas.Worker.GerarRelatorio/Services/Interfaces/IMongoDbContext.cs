using MongoDB.Driver;

namespace ProcessadorTarefas.Worker.GerarRelatorio.Services.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}