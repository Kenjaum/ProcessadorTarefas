using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Infrastructure.Persistence
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var connectionString = config["MongoDbSettings:ConnectionString"];
            var dataBaseName = config["MongoDbSettings:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dataBaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
