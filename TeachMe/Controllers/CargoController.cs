using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Services.Interfaces;

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

        [HttpGet]
        [Route("obter/{id}")]
        [Authorize]
        public ActionResult<CargoDTO> ObterCargoPorId(Guid id)
        {
            _logger.LogDebug("ObterCargoPorId");
            var resultado = _servico.ObterCargoPorId(id);

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<CargoDTO>(resultado))
                : NoContent();
        }

        [HttpGet]
        [Route("obterTodos")]
        [Authorize(Roles = "administrador")]
        public ActionResult<List<CargoDTO>> ObterCargos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _servico.ObterCargos();

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<CargoDTO>(r)).ToList())
                : NoContent();
        }
    }
}
