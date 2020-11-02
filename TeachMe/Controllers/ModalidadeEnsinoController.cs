using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.ViewModel;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Controllers
{
    [Route("api/v1/modalidadeEnsino")]
    [ApiController]
    public class ModalidadeEnsinoController : ControllerBase
    {
        private IModalidadeEnsinoServico _servico;
        private ILogger<ModalidadeEnsinoController> _logger;
        private IMapper _mapper;

        public ModalidadeEnsinoController(IModalidadeEnsinoServico servico, ILogger<ModalidadeEnsinoController> logger, IMapper mapper)
        {
            _servico = servico;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ModalidadeEnsinoViewModel>> ObterTodasModalidades()
        {
            _logger.LogDebug("ObterTodasModalidades");
            var resultado = _servico.ObterTodasModalidades();

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<ModalidadeEnsinoViewModel>(r)).ToList())
                : NoContent();
        }

        [HttpGet]
        [Route("porId")]
        [Authorize]
        public ActionResult<ModalidadeEnsinoViewModel> ObterModalidadePorId(Guid Id)
        {
            _logger.LogDebug("ObterModalidadePorId");
            var resultado = _servico.ObterModalidadePorId(Id);

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<ModalidadeEnsinoViewModel>(resultado))
                : NoContent();
        }
    }
}
