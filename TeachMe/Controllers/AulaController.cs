using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Controllers
{
    [Route("api/v1/aula")]
    [ApiController]
    public class AulaController : ControllerBase
    {
        private readonly IAulaServico _servico;
        private readonly IMapper _mapper;
        private readonly ILogger<AulaController> _logger;

        public AulaController(IAulaServico servico, IMapper mapper, ILogger<AulaController> logger)
        {
            _servico = servico;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("contratarAula")]
        [Authorize]
        public ActionResult<ContratoAulaViewModel> ContratarAula(ContratoAulaDTO contrato)
        {
            _logger.LogDebug("ContratarAula");
            var resultado = _servico.ContratarAula(_mapper.Map<ContratoAula>(contrato));

            return resultado != null 
                ? (ActionResult)Ok(_mapper.Map<ContratoAulaViewModel>(resultado))
                : NoContent();
        }
    }
}
