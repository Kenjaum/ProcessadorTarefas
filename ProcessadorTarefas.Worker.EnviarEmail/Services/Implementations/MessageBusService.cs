using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Channels;
using ProcessadorTarefas.Worker.EnviarEmail.Services.Interfaces;
using ProcessadorTarefas.Worker.EnviarEmail.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProcessadorTarefas.Worker.EnviarEmail.Services.Implementations
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConfiguration config;
        private readonly string? _hostName;
        private readonly string? _nomeExchange;
        private readonly string _routingKey;
        private readonly string _nomeFila;
        private IConnection _connection;
        private readonly IModel _channel;

        public MessageBusService(IConfiguration config)
        {
            this.config = config;
            _hostName = config["RabbitMq:HostName"];
            _nomeExchange = config["RabbitMq:NomeExchange"];
            _routingKey = "EnviarEmail";
            _nomeFila = $"fila.{_routingKey}";

            var factory = new ConnectionFactory { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_nomeExchange, ExchangeType.Direct, durable: true);

            _channel.QueueDeclare(queue: _nomeFila, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: _nomeFila, exchange: _nomeExchange, routingKey: _routingKey);
        }

        public void Publicar(Tarefa tarefa)
        {
            var json = JsonSerializer.Serialize(tarefa);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(_nomeExchange, _routingKey, basicProperties: null, body);
        }

        public void Consumir(Func<string, Task> processarMensagem)
        {
            var consumidor = new EventingBasicConsumer(_channel);
            consumidor.Received += async (modelo, ea) =>
            {
                var corpo = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(corpo);
                await processarMensagem(mensagem);
            };

            _channel.BasicConsume(queue: _nomeFila, autoAck: true, consumer: consumidor);
        }
    }
}
