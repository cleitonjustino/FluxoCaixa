using FluxoCaixa.Application.Domain.Enums;
using FluxoCaixa.Application.Domain.Exceptions;

namespace FluxoCaixa.Application.Domain.Tests
{
    public class ConsolidadoDiarioTests
    {
        [Fact]
        public void AtualizarSaldo_DeveAtualizarSaldoCorretamente()
        {
            // Arrange
            var consolidadoDiario = new ConsolidadoDiario
                .Builder()
                .ComSaldo(TipoLancamento.Credito, 100)
                .Build();

            // Act
            consolidadoDiario.AtualizarSaldo(TipoLancamento.Debito, 50);

            // Assert
            Assert.Equal(50, consolidadoDiario.Saldo);
        }

        [Fact]
        public void AtualizarSaldo_DeveNormalizarSaldoCorretamente()
        {
            // Arrange
            var consolidadoDiario = new ConsolidadoDiario.Builder()
                .ComSaldo(TipoLancamento.Credito, 100)
                .Build();

            // Act
            consolidadoDiario.AtualizarSaldo(TipoLancamento.Debito, 100);

            // Assert
            Assert.Equal(0, consolidadoDiario.Saldo);
        }

        [Fact]
        public void Builder_DeveCriarConsolidadoDiarioCorretamente()
        {
            // Arrange
            var data = new DateTime(2022, 1, 1);

            // Act
            var consolidadoDiario = new ConsolidadoDiario.Builder()
                .SetId()
                .ComData(data)
                .ComSaldo(TipoLancamento.Credito, 100)
                .Build();

            // Assert
            Assert.NotEqual(Guid.Empty, consolidadoDiario.Id);
            Assert.Equal(data, consolidadoDiario.Data);
            Assert.Equal(100, consolidadoDiario.Saldo);
        }


        [Fact]
        public void AtualizarSaldo_ThrowsDomainBaseException_QuandoNovoSaldoENegative()
        {
            // Arrange
            var consolidadoDiario = new ConsolidadoDiario();

            // Act & Assert
            Assert.Throws<DomainBaseException>(() => consolidadoDiario.AtualizarSaldo(TipoLancamento.Credito, -100));
        }

        
        [Fact]
        public void ComSaldo_ThrowsDomainBaseException_QuandoSaldoEInsuficienteParaDebito()
        {
            // Arrange
            var consolidadoDiario = new ConsolidadoDiario.Builder()
                .ComSaldo(TipoLancamento.Credito, 100)
                .Build();

            // Act & Assert
            Assert.Throws<DomainBaseException>(() => consolidadoDiario.AtualizarSaldo(TipoLancamento.Debito, 200));
        }
    }
}
