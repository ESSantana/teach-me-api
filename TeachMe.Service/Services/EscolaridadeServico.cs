using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Core.Exceptions;
using TeachMe.Core.Resources;
using TeachMe.Repository.Repositories.Interfaces;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.Service.Services
{
    public class EscolaridadeServico : IEscolaridadeServico
    {
        private readonly IEscolaridadeRepositorio _repositorio;
        private readonly ILogger<EscolaridadeServico> _logger;
        private readonly IResourceLocalizer _resource;

        public EscolaridadeServico(IEscolaridadeRepositorio repositorio, ILogger<EscolaridadeServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _logger = logger;
            _resource = resource;
        }

        public Escolaridade ObterEscolaridadePorId(Guid Id)
        {
            _logger.LogDebug("ObterEscolaridadePorId");
            if (Id == null || Id == Guid.Empty)
            {
                throw new BusinessException(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id"));
            }

            var resultado = _repositorio.ObterEscolaridadePorId(Id);

            return resultado;
        }

        public List<Escolaridade> ObterTodasEscolaridades()
        {
            _logger.LogDebug("ObterTodasEscolaridades");
            var resultado = _repositorio.ObterTodasEscolaridades();

            return resultado;
        }
    }
}
