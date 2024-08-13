using MediatR;

namespace FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario
{
    public class ObterConsolidadoDiarioQuery : IRequest<List<ObterConsolidadoDiariorReadModel>>
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public ObterConsolidadoDiarioQuery(DateTime dataInicio, DateTime dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
    }
}
