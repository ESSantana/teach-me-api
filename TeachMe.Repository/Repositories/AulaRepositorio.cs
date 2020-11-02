using Microsoft.Extensions.Logging;
using System.Data.Common;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Context;
using TeachMe.Repository.Repositories.Interfaces;

namespace TeachMe.Repository.Repositories
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly TeachDbContext _contexto;
        private readonly ILogger<AulaRepositorio> _logger;

        public AulaRepositorio(TeachDbContext contexto, ILogger<AulaRepositorio> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        public ContratoAula ContratarAula(ContratoAula contrato)
        {
            try
            {
                _contexto.Add(contrato);
                _contexto.SaveChanges();

                return contrato;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"ContratarAula Erro: {ex.Message}");
                throw;
            }
        }
    }
}
