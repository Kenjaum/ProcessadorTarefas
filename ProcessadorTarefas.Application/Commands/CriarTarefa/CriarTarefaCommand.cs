using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.Commands.CriarTarefa
{
    public class CriarTarefaCommand : IRequest<Guid>
    {
        public string Tipo { get; set; }
        public string Dados { get; set; }
    }
}
