using FluxoCaixa.Application.Domain.Enums;

namespace FluxoCaixa.Application.Domain.Events
{
    public class LancamentoEvent 
    {
        public Guid LancamentoId { get;  set; }
        public DateTime Data { get;  set; }
        public decimal Valor { get;  set; }
        public TipoLancamento TipoLancamento { get;  set; }
    }
}
