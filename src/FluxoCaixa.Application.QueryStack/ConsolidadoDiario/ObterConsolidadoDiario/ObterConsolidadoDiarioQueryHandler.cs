using FluxoCaixa.Application.Infrastructure;
using MediatR;
using MongoFramework.Linq;

namespace FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario
{
    public class ObterConsolidadoDiarioQueryHandler : IRequestHandler<ObterConsolidadoDiarioQuery, List<ObterConsolidadoDiariorReadModel>>
    {
        
        private readonly FinanceiroContextMongo _dbContext;

        public ObterConsolidadoDiarioQueryHandler(FinanceiroContextMongo dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<List<ObterConsolidadoDiariorReadModel>> Handle(ObterConsolidadoDiarioQuery request, CancellationToken cancellationToken)
        {
            var resultado = await _dbContext.ConsolidadoDiario
            .Where(c => c.Data >= request.DataInicio && c.Data <= request.DataFim)
            .Select(c => new ObterConsolidadoDiariorReadModel
            {
                Data = c.Data,
                Saldo = c.Saldo
            })
            .ToListAsync();

            return resultado;
        }
    }
}
