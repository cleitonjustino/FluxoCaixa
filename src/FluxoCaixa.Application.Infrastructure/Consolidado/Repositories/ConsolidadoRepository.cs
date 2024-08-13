using FluxoCaixa.Application.Domain;
using FluxoCaixa.Application.Infrastructure.Consolidado.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Application.Infrastructure.Consolidado.Repositories
{
    public class ConsolidadoRepository : IConsolidadoRepository
    {
        private readonly FinanceiroContext _context;

        public ConsolidadoRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<ConsolidadoDiario> ObterPorDataAsync(DateTime data) 
            => await _context.ConsolidadosDiarios.FirstOrDefaultAsync(c => c.Data.Date == data.Date) ?? new ConsolidadoDiario();

        public async Task AtualizarAsync(ConsolidadoDiario consolidado)
        {
            _context.ConsolidadosDiarios.Update(consolidado);
            await _context.SaveChangesAsync();
        }
    }

}
