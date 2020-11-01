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
    public class CargoRepositorio : ICargoRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<CargoRepositorio> _logger;

        public CargoRepositorio(TeachDbContext contexto, ILogger<CargoRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public List<Cargo> ObterCargos()
        {
            _logger.LogDebug("ObterCargos");
            try
            {
                var resultado = _contexto.Set<Cargo>().Where(x => x.Ativo).ToList();
                _logger.LogDebug($"ObterCargos resultado: {resultado.Count} cargos");
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterCargos: {ex.Message}");
                throw;
            }
        }

        public Cargo ObterCargoPorId(Guid id)
        {
            _logger.LogDebug("ObterCargoPorId");
            try
            {
                var resultado = _contexto.Set<Cargo>().FirstOrDefault(x => x.Ativo && x.Id.Equals(id));
                _logger.LogDebug($"ObterCargoPorId resultado sucesso? {resultado != null}");
                return resultado;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ObterCargoPorId: {ex.Message}");
                throw;
            }
        }

    }
}
