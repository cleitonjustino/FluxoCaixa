using FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento;
using FluxoCaixa.Application.Domain.Events;
using MassTransit;
using MediatR;

namespace FluxoCaixa.Application.CommandStack.Consumers
{
    public class LancamentoConsumer : IConsumer<LancamentoEvent>
    {
        private readonly IRequestHandler<CriarConsolidadoDiarioCommand, CriarConsolidadoDiarioResponse> _consolidadoDiarioHandler;

        public LancamentoConsumer(IRequestHandler<CriarConsolidadoDiarioCommand, CriarConsolidadoDiarioResponse> consolidadoDiarioHandler)
        {
            _consolidadoDiarioHandler = consolidadoDiarioHandler;
        }

        public async Task Consume(ConsumeContext<LancamentoEvent> context)
        {
            if (context.Message == null) return;

            var command = new CriarConsolidadoDiarioCommand(context.Message.Valor, context.Message.Data, context.Message.TipoLancamento);
           
            await _consolidadoDiarioHandler.Handle(command, CancellationToken.None);
        }
    }
}
