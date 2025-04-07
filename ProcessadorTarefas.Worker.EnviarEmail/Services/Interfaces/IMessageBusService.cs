using ProcessadorTarefas.Worker.EnviarEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.EnviarEmail.Services.Interfaces
{
    public interface IMessageBusService
    {
        Task Consumir(Func<string, Task> processarMensagem);
        Task Publicar(Tarefa tarefa);
    }
}
