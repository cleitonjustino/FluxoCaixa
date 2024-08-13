using FluxoCaixa.Application.Domain;
using FluxoCaixa.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoFramework.Linq;

namespace FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento
{
    public class CriarConsolidadoDiarioCommandHandler(ILogger<CriarConsolidadoDiarioCommandHandler> logger,
                    FinanceiroContextMongo _context) : IRequestHandler<CriarConsolidadoDiarioCommand, CriarConsolidadoDiarioResponse>
    {
        private readonly ILogger<CriarConsolidadoDiarioCommandHandler> _logger = logger;
        public readonly FinanceiroContextMongo _dbContext = _context;

        public async Task<CriarConsolidadoDiarioResponse> Handle(CriarConsolidadoDiarioCommand request, CancellationToken cancellationToken)
        {
            var startDate = request.Data.Date;
            var endDate = startDate.AddDays(1);

            var consolidadoDia = await _dbContext.ConsolidadoDiario
                .FirstOrDefaultAsync(l => l.Data >= startDate && l.Data < endDate);

            if (consolidadoDia == null)
            {
                _logger.LogInformation("Adicionado consolidado data {Data}", request.Data);

                var consolidadoDiario = new ConsolidadoDiario.Builder()
                                       .SetId()
                                       .ComData(request.Data)
                                       .ComSaldo(request.Tipo, request.Valor)
                                       .Build();

                _dbContext.ConsolidadoDiario.Add(consolidadoDiario);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return CriarResposta(consolidadoDiario.Id, "Sucesso");
            }
            else
            {
                _logger.LogInformation("Adicionado consolidado data {Data}", request.Data);

                consolidadoDia.AtualizarSaldo(request.Tipo, request.Valor);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return CriarResposta(consolidadoDia.Id, "Sucesso");
            }
        }

        private CriarConsolidadoDiarioResponse CriarResposta(Guid id, string status)
        {
            return new CriarConsolidadoDiarioResponse
            {
                Id = id,
                Return = status
            };
        }
    }
}
