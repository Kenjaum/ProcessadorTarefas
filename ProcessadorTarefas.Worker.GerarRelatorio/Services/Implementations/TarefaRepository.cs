using MongoDB.Driver;
using ProcessadorTarefas.Worker.GerarRelatorio.Models;
using ProcessadorTarefas.Worker.GerarRelatorio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.GerarRelatorio.Services.Implementations
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefaRepository(IMongoDbContext context)
        {
            _tarefas = context.GetCollection<Tarefa>("Tarefas");
        }

        public async Task AtualizarStatusAsync(Guid id, StatusTarefa status, int tentativas = 0)
        {
            var filtro = Builders<Tarefa>.Filter.Eq(x => x.Id, id);
            var atualizacao = Builders<Tarefa>.Update
                .Set(t => t.Status, status)
                .Set(t => t.DataUltimaAtualizacao, DateTime.Now)
                .Set(t => t.Tentativas, tentativas);

            await _tarefas.UpdateOneAsync(filtro, atualizacao);
        }
    }
}
