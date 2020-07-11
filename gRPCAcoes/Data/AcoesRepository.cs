using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using gRPCAcoes.Documents;

namespace gRPCAcoes.Data
{
    public class AcoesRepository
    {
        private readonly IConfiguration _configuration;

        public AcoesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Save(AcaoRequest acao)
        {
            var client = new MongoClient(
                _configuration.GetConnectionString("DBAcoesMongoDB"));
            IMongoDatabase db = client.GetDatabase("DBAcoesMongoDB");

            var historico =
                db.GetCollection<AcaoDocument>("HistoricoAcoes");

            var horario = DateTime.Now;
            var document = new AcaoDocument();
            document.HistLancamento = acao.Codigo + horario.ToString("yyyyMMdd-HHmmss");
            document.Codigo = acao.Codigo?.Trim().ToUpper();
            document.Valor = acao.Valor;
            document.Data = horario.ToString("yyyy-MM-dd HH:mm:ss");

            historico.InsertOne(document);
        }
    }
}