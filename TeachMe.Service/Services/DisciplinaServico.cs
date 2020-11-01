using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Resources;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Core.Services
{
    public class DisciplinaServico : IDisciplinaServico
    {
        private readonly IDisciplinaRepositorio _repositorio;
        private readonly ILogger<DisciplinaServico> _logger;
        private readonly IResourceLocalizer _resource;
        public DisciplinaServico(IDisciplinaRepositorio repositorio, ILogger<DisciplinaServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _logger = logger;
            _resource = resource;
        }

        public List<Disciplina> ObterDisciplinas()
        {
            _logger.LogDebug("ObterDisciplinas");
            var resultado = _repositorio.ObterDisciplinas();

            return resultado;
        }

        public Disciplina ObterDisciplinaPorId(Guid id)
        {
            _logger.LogDebug("ObterDisciplinaPorId");

            if (id == null || id == Guid.Empty)
            {
                _logger.LogWarning("ObterDisciplinaPorId com Id inválido");
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var resultado = _repositorio.ObterDisciplinaPorId(id);

            _logger.LogDebug($"ObterDisciplinaPorId resultado sucesso? {resultado != null}");
            return resultado;
        }
    }
}
