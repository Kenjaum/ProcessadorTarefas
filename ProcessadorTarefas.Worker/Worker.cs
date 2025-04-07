using Microsoft.Extensions.DependencyInjection;
using ProcessadorTarefas.Worker.Models;
using ProcessadorTarefas.Worker.Services.Interfaces;
using System.Text.Json;
using System.Threading.Channels;

namespace ProcessadorTarefas.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceBus _serviceBus;
        private readonly ITarefaRepository _repository;
        private readonly ProcessadorDeTarefa _processador;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IServiceBus fila,
            ITarefaRepository repositorio,
            ProcessadorDeTarefa processador,
            ILogger<Worker> logger)
        {
            _serviceBus = fila;
            _repository = repositorio;
            _processador = processador;
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
                    await _processador.ExecutarAsync(tarefa);
                    await _repository.AtualizarStatusAsync(tarefa.Id, StatusTarefa.Concluida);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar tarefa {Id}", tarefa.Id);
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
