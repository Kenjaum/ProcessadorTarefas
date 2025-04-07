using ProcessadorTarefas.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.Strategies
{
    public interface IProcessadorTarefaStrategy
    {
        TipoTarefa Tipo { get; }
        Task ExecutarAsync(Tarefa tarefa);
    }
}
