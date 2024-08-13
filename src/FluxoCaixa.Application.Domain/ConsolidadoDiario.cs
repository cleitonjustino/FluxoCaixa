using FluxoCaixa.Application.Domain.Enums;
using FluxoCaixa.Application.Domain.Exceptions;

namespace FluxoCaixa.Application.Domain
{
    public class ConsolidadoDiario
    {
        public Guid Id { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Saldo { get; private set; }

        public void AtualizarSaldo(TipoLancamento tipo, decimal novoSaldo)
        {
            if (novoSaldo < 0)
            {
                throw new DomainBaseException("O novo saldo não pode ser negativo.");
            }

            if (tipo == TipoLancamento.Credito)
            {
                Saldo += novoSaldo;
            }
            else
            {
                if (Saldo < novoSaldo)
                {
                    throw new DomainBaseException("Saldo insuficiente para realizar o débito.");
                }

                Saldo -= novoSaldo;
            }

            NormalizarSaldo();
        }

        private void NormalizarSaldo()
        {
            if (Saldo == -0.0M)
            {
                Saldo = 0.0M;
            }
        }

        public class Builder
        {
            private readonly ConsolidadoDiario _entidade = new ConsolidadoDiario();

            public Builder SetId()
            {
                _entidade.Id = Guid.NewGuid();
                return this;
            }

            public Builder ComData(DateTime data)
            {
                _entidade.Data = data;
                return this;
            }

            public Builder ComSaldo(TipoLancamento tipo, decimal saldo)
            {
                if (saldo < 0)
                {
                    throw new DomainBaseException("O saldo não pode ser negativo.");
                }

                if (tipo == TipoLancamento.Credito)
                {
                    _entidade.Saldo += saldo;
                }
                else
                {
                    if (_entidade.Saldo < saldo)
                    {
                        throw new DomainBaseException("Saldo insuficiente para realizar o débito.");
                    }

                    _entidade.Saldo -= saldo;
                }

                return this;
            }

            public ConsolidadoDiario Build()
            {
                return _entidade;
            }
        }
    }
}
