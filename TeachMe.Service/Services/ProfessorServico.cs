using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Core.Utils;
using TeachMe.Repository.Entities;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Core.Services
{
    public class ProfessorServico : IProfessorServico
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<ProfessorServico> _logger;

        public ProfessorServico(IProfessorRepositorio repositorio, IUsuarioRepositorio usuarioRepositorio, ILogger<ProfessorServico> logger)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        public List<Professor> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            var resultado = _repositorio.ObterProfessores(id, nome, disciplina);

            _logger.LogDebug($"ObterProfessores resultado: {resultado.Count} professores encontradores");
            return resultado;
        }

        public Professor TornarProfessor(Professor professor, string email, string senha)
        {
            var usuarioDB = _usuarioRepositorio.ObterPorId(professor.UsuarioId, true);
            var credenciaisValidas = SenhaUtils.EncriptarSenha(senha) == usuarioDB.Senha && email == usuarioDB.Email;

            if (!credenciaisValidas)
            {
                throw new BusinessException("");
            }

            return _repositorio.TornarProfessor(professor); 
        }
    }
}
