using ProcessadorTarefas.Core.Entities;
using ProcessadorTarefas.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Threading.Channels;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace ProcessadorTarefas.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConfiguration _config;
        private readonly string? _nomeExchange;
        private readonly string? _nomeFila;

        public MessageBusService(IConfiguration config)
        {
            this._config = config;
            _nomeExchange = _config["RabbitMq:NomeExchange"];
        }

        public async Task Publicar(Tarefa tarefa)
        {
            var factory = new ConnectionFactory { HostName = _config["RabbitMq:HostName"] };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(_nomeExchange, ExchangeType.Direct, durable: true);

            var json = JsonSerializer.Serialize(tarefa);
            var body = Encoding.UTF8.GetBytes(json);

            var routingKey = tarefa.Tipo.ToString();
            await channel.BasicPublishAsync(_nomeExchange, routingKey, body);
        }
    }
}
