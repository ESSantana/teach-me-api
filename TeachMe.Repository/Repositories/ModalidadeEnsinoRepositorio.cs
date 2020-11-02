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
    public class ModalidadeEnsinoRepositorio : IModalidadeEnsinoRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<ModalidadeEnsinoRepositorio> _logger;

        public ModalidadeEnsinoRepositorio(TeachDbContext contexto, ILogger<ModalidadeEnsinoRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public ModalidadeEnsino ObterModalidadePorId(Guid Id)
        {
            try
            {
                var resultado = _contexto.Set<ModalidadeEnsino>().SingleOrDefault(x => x.Id.Equals(Id));
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterEscolaridadePorId error: {ex.Message}");
                throw;
            }
        }

        public List<ModalidadeEnsino> ObterTodasModalidades()
        {
            try
            {
                var resultado = _contexto.Set<ModalidadeEnsino>().ToList();
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterTodasEscolaridades error: {ex.Message}");
                throw;
            }
        }
    }
}
