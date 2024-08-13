using FluxoCaixa.Application.Domain;
using MongoFramework;

namespace FluxoCaixa.Application.Infrastructure
{
    public class FinanceiroContextMongo(IMongoDbConnection connection) : MongoDbContext(connection)
    {
        public MongoDbSet<ConsolidadoDiario> ConsolidadoDiario { get; init; }
        public MongoDbSet<Domain.Lancamento> Lancamento { get; init; }
    }
}
