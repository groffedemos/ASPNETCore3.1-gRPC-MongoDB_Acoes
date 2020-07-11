using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using gRPCAcoes.Data;
using gRPCAcoes.Validators;

namespace gRPCAcoes
{
    public class ControladorAcoesService : ControladorAcoes.ControladorAcoesBase
    {
        private readonly ILogger<ControladorAcoesService> _logger;
        private readonly AcoesRepository _repository;

        public ControladorAcoesService(ILogger<ControladorAcoesService> logger,
            AcoesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override Task<AcaoReply> Registrar(AcaoRequest request, ServerCallContext context)
        {
            _logger.LogInformation(
                $"Dados recebidos: {System.Text.Json.JsonSerializer.Serialize(request)}");

            var validationResult = new AcaoValidator().Validate(request);
            bool dadosValidos = validationResult.IsValid;
            if (dadosValidos)
            {
                _repository.Save(request);
                _logger.LogInformation("Dados de ação registrados com sucesso.");
            }
            else
                _logger.LogInformation("Dados de ação inconsistentes!");

            return Task.FromResult(new AcaoReply
            {
                Sucesso = dadosValidos,
                Descricao = dadosValidos ? "OK" : validationResult.ToString("|") 
            });
        }
    }
}