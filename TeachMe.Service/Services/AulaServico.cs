using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Resources;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.Service.Services
{
    public class AulaServico : IAulaServico
    {
        private readonly IAulaRepositorio _repositorio;
        private readonly ILogger<AulaServico> _logger;
        private readonly IResourceLocalizer _resource;

        public AulaServico(IAulaRepositorio repositorio, ILogger<AulaServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _logger = logger;
            _resource = resource;
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

        public ContratoAula ObterAulaParaAvaliarPorId(long aulaId)
        {
            _logger.LogDebug("ObterAulaParaAvaliarPorId");

            if (aulaId < 1)
            {
                throw new BusinessException(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id da Aula"));
            }

            var aula = _repositorio.ObterAulaParaAvaliarPorId(aulaId);

            return aula;
        }

        public List<ContratoAula> ObterAulaParaAvaliar(long alunoId)
        {
            _logger.LogDebug("ObterAulaParaAvaliar");

            if (alunoId < 1)
            {
                throw new BusinessException(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id do Aluno"));
            }

            var aulas = _repositorio.ObterAulaParaAvaliar(alunoId);

            return aulas;
        }

        public AvaliacaoProfessor AvaliarProfessor(AvaliacaoProfessor avaliacao, long alunoId)
        {
            _logger.LogDebug("AvaliarProfessor");

            var avaliacaoExistente = _repositorio.AulaParaAvaliacao(alunoId, avaliacao.ProfessorId, avaliacao.AulaId);

            if (!avaliacaoExistente)
            {
                throw new BusinessException("Não existe aula para ser avaliada");
            }

            var avaliacaoConcluida = _repositorio.AvaliarProfessor(avaliacao);

            return avaliacaoConcluida;
        }

    }
}
