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
    public class EscolaridadeRepositorio : IEscolaridadeRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<EscolaridadeRepositorio> _logger;

        public EscolaridadeRepositorio(TeachDbContext contexto, ILogger<EscolaridadeRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public Escolaridade ObterEscolaridadePorId(Guid Id)
        {
            try
            {
                var resultado = _contexto.Set<Escolaridade>().SingleOrDefault(x => x.Id.Equals(Id));
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterEscolaridadePorId error: {ex.Message}");
                throw;
            }
        }

        public List<Escolaridade> ObterTodasEscolaridades()
        {
            try
            {
                var resultado = _contexto.Set<Escolaridade>().ToList();
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
