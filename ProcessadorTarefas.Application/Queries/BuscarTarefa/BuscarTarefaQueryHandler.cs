using MediatR;
using ProcessadorTarefas.Application.ViewModels;
using ProcessadorTarefas.Core.Enums;
using ProcessadorTarefas.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.Queries.BuscarTarefa
{
    internal class BuscarTarefaQueryHandler : IRequestHandler<BuscarTarefaQuery, TarefaViewModel>
    {
        private readonly ITarefaRepository _repository;

        public BuscarTarefaQueryHandler(ITarefaRepository repository)
        {
            this._repository = repository;
        }

        public async Task<TarefaViewModel> Handle(BuscarTarefaQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.BuscarTarefaAsync(request.Id);

            var viewModel = new TarefaViewModel()
            {
                Id = entity.Id,
                Tipo = ExibirDescricao(entity.Tipo),
                Dados = entity.Dados,
                Status = entity.Status == Core.Enums.StatusTarefa.EmProcessamento ? "Em Processamento" : entity.Status.ToString(),
                DataCriacao = entity.DataCriacao,
                DataUltimaAtualizacao = entity.DataUltimaAtualizacao
            };

            return viewModel;
        }

        public string ExibirDescricao(TipoTarefa tipo)
        {
            var descricao = "";
            switch (tipo)
            {
                case TipoTarefa.EnviarEmail:
                    descricao = "Enviar Email";
                    break;
                case TipoTarefa.GerarRelatorio:
                    descricao = "Gerar Relatório";
                    break;
                default:
                    break;
            }
            return descricao;
        }
    }

}
