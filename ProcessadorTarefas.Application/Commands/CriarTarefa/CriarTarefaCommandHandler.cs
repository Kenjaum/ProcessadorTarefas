using MediatR;
using ProcessadorTarefas.Core.Entities;
using ProcessadorTarefas.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.Commands.CriarTarefa
{
    public class CriarTarefaCommandHandler : IRequestHandler<CriarTarefaCommand, Guid>
    {
        private readonly ITarefaRepository _repository;

        public CriarTarefaCommandHandler(ITarefaRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Guid> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tarefa(request.Tipo, request.Dados);

            var id = await _repository.CriarTarefaAsync(entity);
            return id;
        }
    }
}
