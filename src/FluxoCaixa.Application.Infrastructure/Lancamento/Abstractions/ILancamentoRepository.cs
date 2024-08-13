namespace FluxoCaixa.Application.Infrastructure.Lancamento.Abstractions
{
    public interface ILancamentoRepository
    {
        Task AdicionarAsync(Domain.Lancamento lancamento);
        Task<IEnumerable<Domain.Lancamento>> ObterPorDataAsync(DateTime data);
    }
}
