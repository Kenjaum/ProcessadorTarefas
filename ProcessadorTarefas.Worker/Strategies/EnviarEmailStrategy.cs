using ProcessadorTarefas.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.Strategies
{
    public class EnviarEmailStrategy : IProcessadorTarefaStrategy
    {
        private readonly ILogger<EnviarEmailStrategy> _logger;

        public EnviarEmailStrategy(ILogger<EnviarEmailStrategy> logger)
        {
            _logger = logger;
        }
        public TipoTarefa Tipo => TipoTarefa.EnviarEmail;

        public async Task ExecutarAsync(Tarefa tarefa)
        {
            _logger.LogInformation($"Enviando e-mail com os dados: {tarefa.Dados}");
        }
    }
}
