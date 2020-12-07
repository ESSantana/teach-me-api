using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;
using TeachMe.Service.Services.Interfaces;

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


        /// <summary>
        /// Pesquisar professor (Necessita de token de autenticação)
        /// </summary>
        /// <param name="id">Opcional: id do professor</param>
        /// <param name="nome">Opcional: nome do professor</param>
        /// <param name="disciplina">Opcional: nome da disciplina</param>
        /// <returns>Lista de professores filtrado pelos parametros</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<List<ProfessorViewModel>> ObterProfessores(long id = 0, string nome = null, string disciplina = null)
        {
            _logger.LogDebug("ObterProfessores");

            var identityContent = User.Identity as System.Security.Claims.ClaimsIdentity;
            long requisitanteId = long.Parse(identityContent.Claims.First(x => x.Type.Equals("id")).Value);

            var resultado = _servico.ObterProfessores(requisitanteId, id, nome, disciplina);

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<ProfessorViewModel>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Aplicar para se tornar um professor (Necessita de token de autenticação)
        /// </summary>
        /// <param name="professor">Dados necessários para se tornar um professor</param>
        /// <returns>Cadastro do usuário como professor na plataforma</returns>
        [HttpPost]
        [Route("aplicarParaProfessor")]
        [Authorize]
        public ActionResult<ProfessorViewModel> TornaProfessor(ProfessorDTO professor)
        {
            _logger.LogDebug("ObterProfessores");
            var resultado = _servico.TornarProfessor(_mapper.Map<Professor>(professor), professor.Email, professor.Senha);

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<ProfessorViewModel>(resultado))
                : NoContent();
        }

        /// <summary>
        /// Obter avaliações feitas para o professor com o id informado
        /// </summary>
        /// <param name="professorId">Id do Professor</param>
        /// <returns>Lista de avaliações do professor</returns>
        [HttpGet]
        [Route("avaliacaoPorProfessor")]
        [Authorize]
        public ActionResult<List<AvaliacaoProfessorViewModel>> ObterAvaliacaoPorIdProfessor(long professorId)
        {
            _logger.LogDebug("ObterAvaliacaoPorIdProfessor");
            var resultado = _servico.ObterAvaliacaoPorIdProfessor(professorId);

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(x => _mapper.Map<AvaliacaoProfessorViewModel>(x)).ToList())
                : NoContent();
        }
    }
}
