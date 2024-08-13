using FluxoCaixa.Application.Domain.Enums;

namespace FluxoCaixa.Application.Domain
{
    public class Lancamento
    {
        public Guid Id { get; private set; }
        public decimal Valor { get; private set; }
        public TipoLancamento Tipo { get; private set; }
        public DateTime Data { get; private set; }
    
        public class Builder
        {
            private readonly Lancamento _entidade = new();

            public Builder SetaId()
            {
                _entidade.Id = Guid.NewGuid();
                _entidade.Data = DateTime.Now;
                return this;
            }

            public Builder ComValor(decimal valor)
            {
                _entidade.Valor = valor;
                return this;
            }

            public Builder ComTipo(TipoLancamento tipo)
            {
                _entidade.Tipo = tipo;
                return this;
            }

            public Builder ComData(DateTime data)
            {
                _entidade.Data = data;
                return this;
            }

            public Lancamento Build()
                    => _entidade;
        }
    }
}