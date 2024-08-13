using FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LancamentosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLancamento([FromBody] CriarLancamentoCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
