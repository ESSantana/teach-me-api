using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class DisciplinaRepositorio : IDisciplinaRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<DisciplinaRepositorio> _logger;

        public DisciplinaRepositorio(TeachDbContext contexto, ILogger<DisciplinaRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public List<Disciplina> ObterDisciplinas()
        {
            _logger.LogDebug("ObterDisciplinas");
            try
            {
                var resultado = _contexto.Set<Disciplina>().Where(x => x.Ativo).ToList();
                _logger.LogDebug($"ObterDisciplinas resultado: {resultado.Count} disciplinas");
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterDisciplinas: {ex.Message}");
                throw;
            }
        }

        public Disciplina ObterDisciplinaPorId(Guid id)
        {
            _logger.LogDebug("ObterDisciplinaPorId");
            try
            {
                var resultado = _contexto.Set<Disciplina>().FirstOrDefault(x => x.Ativo && x.Id.Equals(id));
                _logger.LogDebug($"ObterDisciplinaPorId resultado sucesso? {resultado != null}");
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterDisciplinaPorId: {ex.Message}");
                throw;
            }
        }
    }
}
