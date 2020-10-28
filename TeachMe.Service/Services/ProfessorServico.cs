using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Repository.Entities;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Core.Services
{
    public class ProfessorServico : IProfessorServico
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly ILogger<ProfessorServico> _logger;

        public ProfessorServico(IProfessorRepositorio repositorio, ILogger<ProfessorServico> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public List<Usuario> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            var resultado = _repositorio.ObterProfessores(id, nome, disciplina);

            _logger.LogDebug($"ObterProfessores resultado: {resultado.Count} professores encontradores");
            return resultado;
        }
    }
}
