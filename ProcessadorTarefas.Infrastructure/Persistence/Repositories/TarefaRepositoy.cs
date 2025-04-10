﻿using MongoDB.Driver;
using ProcessadorTarefas.Core.Entities;
using ProcessadorTarefas.Core.Repositories;
using ProcessadorTarefas.Core.Services;

namespace ProcessadorTarefas.Infrastructure.Persistence.Repositories
{
    public class TarefaRepositoy : ITarefaRepository
    {
        private readonly IMongoCollection<Tarefa> _tarefas;
        private readonly IMessageBusService _rabbitMq;

        public TarefaRepositoy(IMongoDbContext context, IMessageBusService rabbitMq)
        {
            _tarefas = context.GetCollection<Tarefa>("Tarefas");
            _rabbitMq = rabbitMq;
        }

        public async Task<Guid> CriarTarefaAsync(Tarefa entity)
        {
            await _tarefas.InsertOneAsync(entity);
            await _rabbitMq.Publicar(entity);
            return entity.Id;
        }

        public async Task<Tarefa> BuscarTarefaAsync(Guid id)
        {
            return await _tarefas.Find(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}
