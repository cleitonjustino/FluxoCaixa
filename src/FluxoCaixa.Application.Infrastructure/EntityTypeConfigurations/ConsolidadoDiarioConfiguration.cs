using FluxoCaixa.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Application.Infrastructure.EntityTypeConfigurations
{
    public class ConsolidadoDiarioConfiguration : IEntityTypeConfiguration<ConsolidadoDiario>
    {
        public void Configure(EntityTypeBuilder<ConsolidadoDiario> builder)
        {
            builder.ToTable("ConsolidadoDiario");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier"); ;

            builder.Property(e => e.Saldo)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Data);
        }
    }
}
