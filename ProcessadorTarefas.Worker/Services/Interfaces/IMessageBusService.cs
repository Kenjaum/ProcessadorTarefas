using ProcessadorTarefas.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Core.Services
{
    public interface IMessageBusService
    {
        Task Consumir(Func<string, Task> processarMensagem);
        Task Publicar(Tarefa tarefa);
    }
}
