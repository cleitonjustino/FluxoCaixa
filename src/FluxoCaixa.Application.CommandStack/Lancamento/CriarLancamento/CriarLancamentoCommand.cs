using FluxoCaixa.Application.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento
{
    public class CriarLancamentoCommand : IRequest<CriarLancamentoResponse>
    {
        [Range(0.01, 1000000.00, ErrorMessage = "O valor deve estar entre 0.01 e 1,000,000.00.")]
        public decimal Valor { get; set; }

        [Required]
        public TipoLancamento Tipo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }
    }
}
