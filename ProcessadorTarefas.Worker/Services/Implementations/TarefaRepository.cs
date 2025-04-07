using MongoDB.Driver;
using ProcessadorTarefas.Worker.Models;
using ProcessadorTarefas.Worker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.Services.Implementations
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly IMongoCollection<Tarefa> _collection;

        public TarefaRepository()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var banco = cliente.GetDatabase("ProcessadorTarefas");
            _collection = banco.GetCollection<Tarefa>("Tarefas");
        }

        public async Task AtualizarStatusAsync(Guid id, StatusTarefa status, int tentativas = 0)
        {
            var filtro = Builders<Tarefa>.Filter.Eq(x => x.Id, id);
            var atualizacao = Builders<Tarefa>.Update
                .Set(t => t.Status, status)
                .Set(t => t.Tentativas, tentativas);

            await _collection.UpdateOneAsync(filtro, atualizacao);
        }
    }
}
