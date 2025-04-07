using MediatR;
using ProcessadorTarefas.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.Queries.BuscarTarefa
{
    public class BuscarTarefaQuery : IRequest<TarefaViewModel>
    {
        public BuscarTarefaQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
