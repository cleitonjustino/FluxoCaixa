using FluxoCaixa.Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Application.Infrastructure
{
    public class FinanceiroContext(DbContextOptions<FinanceiroContext> options) : DbContext(options)
    {
        public DbSet<Domain.Lancamento> Lancamentos { get; set; }
        public DbSet<ConsolidadoDiario> ConsolidadosDiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsolidadoDiario>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ConsolidadoDiario>()
                .Property(c => c.Saldo)
                .HasColumnType("decimal(18,2)"); // Define o tipo de coluna decimal

            modelBuilder.Entity<Domain.Lancamento>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Domain.Lancamento>()
                .Property(l => l.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}
