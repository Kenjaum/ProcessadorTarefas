using Microsoft.Extensions.DependencyInjection;
using ProcessadorTarefas.Worker.GerarRelatorio.Models;
using ProcessadorTarefas.Worker.GerarRelatorio.Services.Interfaces;
using System.Text.Json;
using System.Threading.Channels;

namespace ProcessadorTarefas.Worker.GerarRelatorio
{
    public class Worker : BackgroundService
    {
        private readonly IMessageBusService _serviceBus;
        private readonly ITarefaRepository _repository;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IMessageBusService fila,
            ITarefaRepository repositorio,
            ILogger<Worker> logger)
        {
            _serviceBus = fila;
            _repository = repositorio;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _serviceBus.Consumir(async (mensagem) =>
            {
                var tarefa = JsonSerializer.Deserialize<Tarefa>(mensagem);
                if (tarefa == null) return;

                try
                {
                    await _repository.AtualizarStatusAsync(tarefa.Id, StatusTarefa.EmProcessamento);
                    _logger.LogInformation($"Gerando relatório com os dados: {tarefa.Dados}");

                    _logger.LogInformation($"Relatório gerado com sucesso");
                    await _repository.AtualizarStatusAsync(tarefa.Id, StatusTarefa.Concluida);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro ao processar tarefa {tarefa.Id} em ProcessadorTarefas.Worker.GerarRelatorio");
                    tarefa.Tentativas++;

                    if (tarefa.Tentativas >= 3)
                    {
                        await _repository.AtualizarStatusAsync(tarefa.Id, StatusTarefa.Erro, tarefa.Tentativas);
                    }
                    else
                    {
                        await _repository.AtualizarStatusAsync(tarefa.Id, StatusTarefa.Pendente, tarefa.Tentativas);
                        _serviceBus.Publicar(tarefa);
                    }
                }
            });

            return Task.CompletedTask;
        }
    }
}
