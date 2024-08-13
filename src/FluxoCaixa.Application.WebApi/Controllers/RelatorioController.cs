using FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace FluxoCaixa.Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("saldo-diario")]
        public async Task<IActionResult> GetSaldoDiarioConsolidado([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, CancellationToken cancellationToken)
        {
            var filter = new ObterConsolidadoDiarioQuery(dataInicio, dataFim);
            var relatorio = await _mediator.Send(filter, cancellationToken);
            //var relatorio = await _relatorioService.GerarRelatorioSaldoDiarioConsolidado(dataInicio, dataFim);
            return Ok(relatorio);
        }

        [HttpGet("saldo-diario-pdf")]
        public async Task<IActionResult> GetSaldoDiarioConsolidadoPdf([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, CancellationToken cancellationToken)
        {
            var filter = new ObterConsolidadoDiarioPdfQuery(dataInicio, dataFim);
            var pdfBytes = await _mediator.Send(filter, cancellationToken);
            return File(pdfBytes, "application/pdf", "RelatorioSaldoDiario.pdf");
        }
    }
}
