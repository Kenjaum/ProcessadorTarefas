using ProcessadorTarefas.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Core.Repositories
{
    public interface ITarefaRepository
    {
        Task<Tarefa> BuscarTarefaAsync(Guid id);
        Task<Guid> CriarTarefaAsync(Tarefa entity);
    }
}
