using FluxoCaixa.Application.Domain.Enums;
using MediatR;

namespace FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento
{
    public class CriarConsolidadoDiarioCommand : IRequest<CriarConsolidadoDiarioResponse>
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public TipoLancamento Tipo { get; set; }

        public CriarConsolidadoDiarioCommand(decimal valor, DateTime data, TipoLancamento tipo)
        {
            Valor = valor;
            Data = data;
            Tipo = tipo ;
        }
    }
}
