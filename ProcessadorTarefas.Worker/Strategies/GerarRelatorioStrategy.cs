using ProcessadorTarefas.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.Strategies
{
    public class GerarRelatorioStrategy : IProcessadorTarefaStrategy
    {
        private readonly ILogger<GerarRelatorioStrategy> _logger;

        public GerarRelatorioStrategy(ILogger<GerarRelatorioStrategy> logger)
        {
            _logger = logger;
        }

        public TipoTarefa Tipo => TipoTarefa.GerarRelatorio;

        public async Task ExecutarAsync(Tarefa tarefa)
        {
            _logger.LogInformation($"Gerando relatório com os dados: {tarefa.Dados}");
        }
    }
}
