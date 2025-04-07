using ProcessadorTarefas.Worker.GerarRelatorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.GerarRelatorio.Services.Interfaces
{
    public interface IMessageBusService
    {
        void Consumir(Func<string, Task> processarMensagem);
        void Publicar(Tarefa tarefa);
    }
}
