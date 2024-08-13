using FluxoCaixa.Application.Domain;
using FluxoCaixa.Application.Domain.Enums;
using Xunit;

namespace FluxoCaixa.Application.Tests
{
    public class LancamentoTests
    {
        [Fact]
        public void Builder_SetProperties_ReturnsLancamentoComValoresCorretos()
        {
            // Arrange
            decimal valor = 100.50m;
            TipoLancamento tipo = TipoLancamento.Credito;
            var data = new DateTime(2022, 1, 1);

            // Act
            var lancamento = new Lancamento.Builder()
                .ComValor(valor)
                .ComTipo(tipo)
                .ComData(data)
                .Build();

            // Assert
            Assert.Equal(valor, lancamento.Valor);
            Assert.Equal(tipo, lancamento.Tipo);
            Assert.Equal(data, lancamento.Data);
        }
    }
}
