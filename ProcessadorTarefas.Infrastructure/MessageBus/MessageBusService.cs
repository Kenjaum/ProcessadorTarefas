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

namespace ProcessadorTarefas.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {

        public MessageBusService()
        {

        }

        public async Task Publish(Tarefa tarefa)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "fila-tarefa", durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(tarefa);
            var body = Encoding.UTF8.GetBytes(json);
            await channel.BasicPublishAsync("", "fila-tarefa", body);
        }
    }
}
