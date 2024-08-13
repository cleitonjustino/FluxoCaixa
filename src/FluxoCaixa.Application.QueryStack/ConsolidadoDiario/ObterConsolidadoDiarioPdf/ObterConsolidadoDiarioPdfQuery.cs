using MediatR;

namespace FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario
{
    public class ObterConsolidadoDiarioPdfQuery : IRequest<byte[]>
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public ObterConsolidadoDiarioPdfQuery(DateTime dataInicio, DateTime dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
    }
}
