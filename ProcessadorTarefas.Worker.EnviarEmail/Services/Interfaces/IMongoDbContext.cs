using MongoDB.Driver;

namespace ProcessadorTarefas.Worker.EnviarEmail.Services.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}