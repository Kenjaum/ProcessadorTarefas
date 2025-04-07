using ProcessadorTarefas.Worker.Models;
using ProcessadorTarefas.Worker.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.Services.Implementations
{
    public class ProcessadorTarefaService
    {
        private readonly IDictionary<TipoTarefa, IProcessadorTarefaStrategy> _strategies;

        public ProcessadorTarefaService(IEnumerable<IProcessadorTarefaStrategy> estrategias)
        {
            _strategies = new Dictionary<TipoTarefa, IProcessadorTarefaStrategy>();
            foreach (var estrategia in estrategias)
            {
                _strategies[estrategia.Tipo] = estrategia;
            }
        }

        public async Task ExecutarAsync(Tarefa tarefa)
        {
            if (_strategies.TryGetValue(tarefa.Tipo, out var estrategia))
            {
                await estrategia.ExecutarAsync(tarefa);
            }
            else
            {
                throw new NotSupportedException($"Nenhuma estratégia encontrada para o tipo de tarefa '{tarefa.Tipo}'.");
            }
        }
    }
}
