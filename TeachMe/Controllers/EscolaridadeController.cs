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
    [Route("api/v1/escolaridade")]
    [ApiController]
    public class EscolaridadeController : ControllerBase
    {
        private IEscolaridadeServico _servico;
        private ILogger<EscolaridadeController> _logger;
        private IMapper _mapper;

        public EscolaridadeController(IEscolaridadeServico servico, ILogger<EscolaridadeController> logger, IMapper mapper)
        {
            _servico = servico;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todas escolaridades cadastradas (Necessita de token de autenticação)
        /// </summary>
        /// <returns>Lista de escolaridade</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<List<EscolaridadeViewModel>> ObterTodasEscolaridades()
        {
            _logger.LogDebug("ObterTodasEscolaridades");
            var resultado = _servico.ObterTodasEscolaridades();

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<EscolaridadeViewModel>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Obter escolaridade por Id (Necessita de token de autenticação)
        /// </summary>
        /// <param name="Id">Id da escolaridade</param>
        /// <returns>Escolaridade</returns>
        [HttpGet]
        [Route("porId")]
        [Authorize]
        public ActionResult<EscolaridadeViewModel> ObterEscolaridadePorId(Guid Id)
        {
            _logger.LogDebug("ObterEscolaridadePorId");
            var resultado = _servico.ObterEscolaridadePorId(Id);

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<EscolaridadeViewModel>(resultado))
                : NoContent();
        }

    }
}
