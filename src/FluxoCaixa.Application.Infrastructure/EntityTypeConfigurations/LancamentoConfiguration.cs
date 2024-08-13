using FluxoCaixa.Application.Domain.Enums;
using FluxoCaixa.Application.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Application.Infrastructure.EntityTypeConfigurations
{
    public class LancamentoConfiguration : IEntityTypeConfiguration<Domain.Lancamento>
    {
        public void Configure(EntityTypeBuilder<Domain.Lancamento> builder)
        {
            builder.ToTable("Lancamento");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier"); ;

            builder.Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Data);

            builder.Property(e => e.Tipo)
                .HasMaxLength(1)
                .HasConversion(new CharEnumToStringConverter<TipoLancamento>());
        }
    }
}
