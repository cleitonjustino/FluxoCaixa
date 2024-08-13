using FluxoCaixa.Application.Infrastructure.Lancamento.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Application.Infrastructure.Lancamento.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
        private readonly FinanceiroContext _context;

        public LancamentoRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Domain.Lancamento lancamento)
        {
            await _context.Lancamentos.AddAsync(lancamento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Lancamento>> ObterPorDataAsync(DateTime data)
        {
            return await _context.Lancamentos.Where(l => l.Data == data).ToListAsync();
        }
    }
}
