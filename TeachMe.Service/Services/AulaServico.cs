using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IEmailRepositorio _emailRepositorio;
        private readonly ILogger<AulaServico> _logger;
        private readonly IResourceLocalizer _resource;

        #region EmailInfo
        private readonly string mensagemAluno = "Você acaba de contratar um serviço de aula, para entrar em contato com o professor, utilize as informações abaixo.";
        private readonly string mensagemProfessor = "Seu serviço de aula acaba de ser contratado, para entrar em contato com o contratante utilize as informações abaixo.";
        private string MESSAGEM_CONTRATO
        {
            get
            {
                return "<strong>Contrato de Aula</strong><br>" +
                    "{0}<br>" +
                    "E-mail: {1}<br>" +
                    "Telefone: {2}";
            }
        }
        #endregion

        public AulaServico(IAulaRepositorio repositorio, IUsuarioRepositorio usuarioRepositorio, IProfessorRepositorio professorRepositorio, IEmailRepositorio emailRepositorio, ILogger<AulaServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _professorRepositorio = professorRepositorio;
            _emailRepositorio = emailRepositorio;
            _logger = logger;
            _resource = resource;
        }

        public ContratoAula ContratarAula(ContratoAula contrato)
        {
            _logger.LogDebug("ContratarAula");


            contrato.DataContrato = DateTime.Now;
            contrato.DataFimPrestacao = contrato.DataInicioPrestacao.Value.AddHours(contrato.HorasContratadas);
            contrato.ValorTotal = contrato.ValorHora * contrato.HorasContratadas;
            contrato.Avaliado = false;

            if(!_repositorio.IsProfessorDisponivel(contrato.ProfessorId, contrato.DataInicioPrestacao ?? DateTime.Now, contrato.DataFimPrestacao ?? DateTime.Now))
            {
                throw new BusinessException(_resource.GetString("UNAVAILABLE_PERIOD"));
            }

            var contratoSalvo = _repositorio.ContratarAula(contrato);

            var aluno = _usuarioRepositorio.Obter(x => new List<long> { contrato.AlunoId, contrato.ProfessorId }.Any(y => y == x.Id)).First();
            var professor = _professorRepositorio.ObterProfessores(contrato.ProfessorId).First().Usuario;

            EnviarNotificacaoContrato(aluno, professor, mensagemAluno);
            EnviarNotificacaoContrato(professor, aluno, mensagemProfessor);

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

        private void EnviarNotificacaoContrato(Usuario destinatario, Usuario participante, string mensagem)
        {
            var corpoEmail = string.Format(MESSAGEM_CONTRATO, mensagem, participante.Email, participante.Telefone);
            _emailRepositorio.EnviarEmail(destinatario.Email, "Contrato de Aula", corpoEmail);

        }
    }
}
