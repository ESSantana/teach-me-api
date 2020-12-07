using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Resources;
using TeachMe.Core.Utils;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.Service.Services
{
    public class ProfessorServico : IProfessorServico
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEmailRepositorio _emailRepositorio;
        private readonly ILogger<ProfessorServico> _logger;
        private readonly IResourceLocalizer _resource;

        public ProfessorServico(IProfessorRepositorio repositorio, IUsuarioRepositorio usuarioRepositorio, IEmailRepositorio emailRepositorio, ILogger<ProfessorServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _emailRepositorio = emailRepositorio;
            _logger = logger;
            _resource = resource;
        }

        public List<Professor> ObterProfessores(long requisitanteId, long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            var resultado = _repositorio.ObterProfessores(requisitanteId, id, nome, disciplina);

            _logger.LogDebug($"ObterProfessores resultado: {resultado.Count} professores encontradores");
            return resultado;
        }

        public Professor TornarProfessor(Professor professor, string email, string senha)
        {
            var usuarioDB = _usuarioRepositorio.ObterPorId(professor.UsuarioId, true);
            var credenciaisValidas = SenhaUtils.EncriptarSenha(senha) == usuarioDB.Senha && email == usuarioDB.Email;

            if (!credenciaisValidas)
            {
                throw new BusinessException(_resource.GetString("INVALID_CREDENTIALS"));
            }

            var resultado = _repositorio.TornarProfessor(professor);

            if (resultado != null)
            {
                _emailRepositorio.NotificarMudancaPerfilProfessor(usuarioDB.Email, usuarioDB.Nome);
            }

            return resultado;
        }

        public List<AvaliacaoProfessor> ObterAvaliacaoPorIdProfessor(long professorId)
        {
            _logger.LogInformation(nameof(ObterAvaliacaoPorIdProfessor));

            if (professorId < 1)
            {
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var resultado = _repositorio.ObterAvaliacaoPorIdProfessor(professorId);

            return resultado;
        }
    }
}
