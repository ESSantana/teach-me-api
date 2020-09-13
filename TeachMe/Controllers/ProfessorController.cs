using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Services.Interfaces;

namespace TeachMe.API.Controllers
{
    [Route("api/v1/professor")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorServico _servico;
        private readonly ILogger<ProfessorController> _logger;
        private readonly IMapper _mapper;

        public ProfessorController(IProfessorServico servico, ILogger<ProfessorController> logger, IMapper mapper)
        {
            _servico = servico;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<UsuarioDTO>> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");
            var resultado = _servico.ObterProfessores(id, nome, disciplina);

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<UsuarioDTO>(r)).ToList())
                : NoContent();
        }
    }
}
