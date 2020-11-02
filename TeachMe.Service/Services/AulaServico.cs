using Microsoft.Extensions.Logging;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.Service.Services
{
    public class AulaServico : IAulaServico
    {
        private readonly IAulaRepositorio _repositorio;
        private readonly ILogger<AulaServico> _logger;

        public AulaServico(IAulaRepositorio repositorio, ILogger<AulaServico> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public ContratoAula ContratarAula(ContratoAula contrato)
        {
            _logger.LogDebug("ContratarAula");

            contrato.DataFimPrestacao = contrato.DataInicioPrestacao.Value.AddHours(contrato.HorasContratadas);

            var contratoSalvo = _repositorio.ContratarAula(contrato);

            return contratoSalvo.Id != 0
                ? contratoSalvo
                : null;
        }
    }
}
