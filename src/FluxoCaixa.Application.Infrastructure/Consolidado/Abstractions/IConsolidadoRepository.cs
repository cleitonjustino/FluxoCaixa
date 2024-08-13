using FluxoCaixa.Application.Domain;

namespace FluxoCaixa.Application.Infrastructure.Consolidado.Abstractions
{
    public interface IConsolidadoRepository
    {
        Task<ConsolidadoDiario> ObterPorDataAsync(DateTime data);
        Task AtualizarAsync(ConsolidadoDiario consolidado);
    }
}
