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
    [Route("api/v1/cargo")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoServico _servico;
        private readonly ILogger<CargoController> _logger;
        private readonly IMapper _mapper;
        public CargoController(ICargoServico servico, ILogger<CargoController> logger, IMapper mapper)
        {
            _servico = servico;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter todos os cargos cadastrados (Acesso só para administrador)
        /// </summary>
        /// <returns>Lista de Cargos</returns>
        [HttpGet]
        [Authorize(Roles = "administrador")]
        public ActionResult<List<CargoViewModel>> ObterCargos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _servico.ObterCargos();

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<CargoViewModel>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Obter cargo por Id (Necessita de token de autenticação)
        /// </summary>
        /// <param name="Id">Id do Cargo</param>
        /// <returns>Cargo</returns>
        [HttpGet]
        [Route("porId")]
        [Authorize]
        public ActionResult<CargoViewModel> ObterCargoPorId(Guid Id)
        {
            _logger.LogDebug("ObterCargoPorId");
            var resultado = _servico.ObterCargoPorId(Id);

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<CargoViewModel>(resultado))
                : NoContent();
        }
    }
}
