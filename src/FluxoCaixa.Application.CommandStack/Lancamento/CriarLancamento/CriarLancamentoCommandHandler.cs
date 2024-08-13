using FluxoCaixa.Application.Domain.Events;
using FluxoCaixa.Application.Infrastructure;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento
{
    public class CriarLancamentoCommandHandler(ILogger<CriarLancamentoCommandHandler> logger,
                FinanceiroContextMongo _context, IBus messageBus) : IRequestHandler<CriarLancamentoCommand, CriarLancamentoResponse>
    {
        private const string QueueConsolidado = "queue:consolidado-diario";

        private readonly IBus _messageBus = messageBus;
        private readonly ILogger<CriarLancamentoCommandHandler> _logger = logger;
        public readonly FinanceiroContextMongo _dbContext = _context;

        public async Task<CriarLancamentoResponse> Handle(CriarLancamentoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lancamento = new Domain.Lancamento.Builder()
                    .SetaId()
                    .ComValor(request.Valor)
                    .ComTipo(request.Tipo)
                    .ComData(request.Data)
                    .Build();

                _dbContext.Lancamento.Add(lancamento);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Lançamento criado com sucesso. Id: {LancamentoId}", lancamento.Id);

                await EnviarEventoConsolidadoDiario(lancamento);

                return new CriarLancamentoResponse
                {
                    Id = lancamento.Id,
                    Return = "Success"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao gravar lançamento. Valor: {Valor}, Tipo: {Tipo}, Data: {Data}",
                    request.Valor, request.Tipo, request.Data);

                return new CriarLancamentoResponse
                {
                    Id = Guid.Empty,
                    Return = $"Error: {ex.Message}"
                };
            }
        }

        private async Task EnviarEventoConsolidadoDiario(Domain.Lancamento lancamento)
        {
            try
            {
                var model = new LancamentoEvent
                {
                    LancamentoId = lancamento.Id,
                    Valor =  lancamento.Valor,
                    Data = lancamento.Data,
                    TipoLancamento = lancamento.Tipo
                };
                var endpoint = await _messageBus.GetSendEndpoint(new Uri(QueueConsolidado));
                await endpoint.Send(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao enviar evento consolidado diário. LancamentoId: {LancamentoId}", lancamento.Id);
            }
        }
    }
}
