using ProcessadorTarefas.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Core.Services
{
    public interface IMessageBusService
    {
        Task Publish(Tarefa tarefa);
    }
}
