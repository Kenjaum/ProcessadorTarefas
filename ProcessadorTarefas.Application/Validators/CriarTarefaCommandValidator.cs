using FluentValidation;
using ProcessadorTarefas.Application.Commands.CriarTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.Validators
{
    public class CriarTarefaCommandValidator : AbstractValidator<CriarTarefaCommand>
    {
        public CriarTarefaCommandValidator()
        {
            RuleFor(p => p.Dados).NotEmpty().NotNull().WithMessage("O campo Dados é obrigatório");
            RuleFor(p => p.Tipo).IsInEnum().WithMessage("Tipo inválido");
        }
    }
}
