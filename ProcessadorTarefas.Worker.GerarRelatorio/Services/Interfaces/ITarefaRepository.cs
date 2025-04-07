using ProcessadorTarefas.Worker.EnviarEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.EnviarEmail.Services.Interfaces
{
    public interface ITarefaRepository
    {
        Task AtualizarStatusAsync(Guid id, StatusTarefa status, int tentativas = 0);
    }
}
