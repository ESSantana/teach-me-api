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
    public class CargoServico : ICargoServico
    {
        private readonly ICargoRepositorio _repositorio;
        private readonly ILogger<CargoServico> _logger;
        private readonly IResourceLocalizer _resource;
        public CargoServico(ICargoRepositorio repositorio, ILogger<CargoServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _logger = logger;
            _resource = resource;
        }


        public List<Cargo> ObterCargos()
        {
            _logger.LogDebug("ObterCargos");
            var resultado = _repositorio.ObterCargos();

            return resultado;
        }

        public Cargo ObterCargoPorId(Guid id)
        {
            _logger.LogDebug("ObterCargoPorId");

            if(id == null || id == Guid.Empty)
            {
                _logger.LogWarning("ObterCargoPorId com Id inválido");
                throw new BusinessException(_resource.GetString("INVALID_ID"));
            }

            var resultado = _repositorio.ObterCargoPorId(id);

            _logger.LogDebug($"ObterCargoPorId resultado sucesso? {resultado != null}");
            return resultado;
        }
    }
}
