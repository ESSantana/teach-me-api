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
    public class ModalidadeEnsinoServico : IModalidadeEnsinoServico
    {
        private readonly IModalidadeEnsinoRepositorio _repositorio;
        private readonly ILogger<ModalidadeEnsinoServico> _logger;
        private readonly IResourceLocalizer _resource;

        public ModalidadeEnsinoServico(IModalidadeEnsinoRepositorio repositorio, ILogger<ModalidadeEnsinoServico> logger, IResourceLocalizer resource)
        {
            _repositorio = repositorio;
            _logger = logger;
            _resource = resource;
        }

        public ModalidadeEnsino ObterModalidadePorId(Guid Id)
        {
            _logger.LogDebug("ObterModalidadePorId");
            if (Id == null || Id == Guid.Empty)
            {
                throw new BusinessException(string.Format(_resource.GetString("FIELD_REQUIRED"), "Id"));
            }

            var resultado = _repositorio.ObterModalidadePorId(Id);

            return resultado;
        }

        public List<ModalidadeEnsino> ObterTodasModalidades()
        {
            _logger.LogDebug("ObterTodasModalidades");
            var resultado = _repositorio.ObterTodasModalidades();

            return resultado;
        }
    }
}
