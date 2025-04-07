using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProcessadorTarefas.Application.Commands.CriarTarefa;
using ProcessadorTarefas.Application.Queries.BuscarTarefa;

namespace ProcessadorTarefas.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TarefaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa(CriarTarefaCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CriarTarefa), new { id = id }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarTarefa(Guid id)
        {
            var tarefa = await _mediator.Send(new BuscarTarefaQuery(id));
            return tarefa is null ? NotFound() : Ok(tarefa);
        }
    }
}
