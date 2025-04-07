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
using ProcessadorTarefas.Worker.Models;
using RabbitMQ.Client.Events;

namespace ProcessadorTarefas.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConfiguration config;
        private readonly string? hostName;
        private readonly string? nomeFila;

        public MessageBusService(IConfiguration config)
        {
            this.config = config;
            hostName = config["RabbitMq:HostName"];
            nomeFila = config["RabbitMq:NomeFila"];
        }

        public async Task Publicar(Tarefa tarefa)
        {
            var factory = new ConnectionFactory { HostName = hostName };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: nomeFila, durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(tarefa);
            var body = Encoding.UTF8.GetBytes(json);
            await channel.BasicPublishAsync("", nomeFila, body);
        }

        public async Task Consumir(Func<string, Task> processarMensagem)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var consumidor = new AsyncEventingBasicConsumer(channel);
            consumidor.ReceivedAsync += async (model, ea) =>
            {
                var corpo = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(corpo);

                await processarMensagem(mensagem);
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(queue: "fila-tarefa", autoAck: false, consumer: consumidor);
        }
    }
}
