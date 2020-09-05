using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Services.Interfaces;
using TeachMe.Repository.Entities;

namespace TeachMe.API.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioServico _servico;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioServico servico, ILogger<UsuarioController> logger, IMapper mapper)
        {
            _servico = servico;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("obterTodos")]
        [Authorize]
        public ActionResult<List<UsuarioDTO>> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _servico.ObterTodos();

            _logger.LogDebug($"ObterTodos resultado: {resultado.Count} usuário(s)");

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<UsuarioDTO>(r)).ToList())
                : NoContent();
        }

        [HttpGet]
        [Route("obter/{id}")]
        [Authorize]
        public ActionResult<UsuarioDTO> Obter(long id)
        {
            _logger.LogDebug("Obter");
            var resultado = _servico.ObterPorId(id);

            _logger.LogDebug($"Obter usuário com sucesso? {resultado != null}");

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<UsuarioDTO>(resultado))
                : NoContent();
        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public ActionResult<object> Cadastrar(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            _logger.LogDebug("Cadastrar");
            var resultado = _servico.Cadastrar(usuario);

            _logger.LogDebug($"Cadastrar: {resultado} usuário cadastrado");

            return resultado > 0
                ? (ActionResult)Ok(new { UsuarioCadastrado = resultado > 0 })
                : NoContent();
        }

        [HttpPost]
        [Route("alterar")]
        [Authorize(Roles = "aluno,professor,administrador")]
        public ActionResult<UsuarioDTO> Alterar(UsuarioDTO usuarioDto)
        {
            var exampleEntity = _mapper.Map<Usuario>(usuarioDto);

            _logger.LogDebug("Alterar");
            var resultado = _servico.Alterar(exampleEntity);

            _logger.LogDebug($"Alterado com sucesso? {resultado != null}");

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<UsuarioDTO>(resultado))
                : NoContent();
        }

        [HttpDelete]
        [Route("excluir/{id}")]
        [Authorize(Roles = "administrador")]
        public ActionResult<object> Excluir(long id)
        {
            _logger.LogDebug("Excluir");
            var resultado = _servico.Excluir(id);

            _logger.LogDebug($"Excluir: {resultado} cadastro de usuário deletado");

            return Ok(new { UsuarioDeletado = resultado > 0 });
        }
    }
}
