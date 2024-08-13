using FluxoCaixa.Application.Infrastructure;
using MediatR;
using MongoFramework.Linq;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario
{
    public class ObterConsolidadoDiarioPdfQueryHandler : IRequestHandler<ObterConsolidadoDiarioPdfQuery, byte[]>
    {

        private readonly FinanceiroContextMongo _dbContext;
        private readonly IConverter _converter;

        public ObterConsolidadoDiarioPdfQueryHandler(FinanceiroContextMongo dbContext, IConverter converter)
        {
            _dbContext = dbContext;
            _converter = converter;
        }

        public async Task<byte[]> Handle(ObterConsolidadoDiarioPdfQuery request, CancellationToken cancellationToken)
        {
            var dados = await GerarRelatorioSaldoDiarioConsolidado(request.DataInicio, request.DataFim);

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = { ColorMode = ColorMode.Color, Orientation = Orientation.Portrait },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = GeraHtml(dados),
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(pdf);
        }

        private string GeraHtml(List<ObterConsolidadoDiarioPdfReadModel> dados)
        {
            var html = "<h1>Relatório de Saldo Diário Consolidado</h1>";
            html += "<table><thead><tr><th>Data</th><th>Saldo</th></tr></thead><tbody>";

            foreach (var item in dados)
            {
                html += $"<tr><td>{item.Data:yyyy-MM-dd}</td><td>{item.Saldo:C}</td></tr>";
            }

            html += "</tbody></table>";
            return html;
        }

        public async Task<List<ObterConsolidadoDiarioPdfReadModel>> GerarRelatorioSaldoDiarioConsolidado(DateTime dataInicio, DateTime dataFim)
        {
            var resultado = await _dbContext.ConsolidadoDiario
                .Where(c => c.Data >= dataInicio && c.Data <= dataFim)
                .Select(c => new ObterConsolidadoDiarioPdfReadModel
                {
                    Data = c.Data,
                    Saldo = c.Saldo
                })
                .ToListAsync();

            return resultado;
        }
    }
}
