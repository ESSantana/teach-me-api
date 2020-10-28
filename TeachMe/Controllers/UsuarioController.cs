using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
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
        public ActionResult<List<UsuarioViewModel>> ObterTodos()
        {
            _logger.LogDebug("ObterTodos");
            var resultado = _servico.ObterTodos();

            _logger.LogDebug($"ObterTodos resultado: {resultado.Count} usuário(s)");

            return resultado.Count > 0
                ? (ActionResult)Ok(resultado.Select(r => _mapper.Map<UsuarioViewModel>(r)).ToList())
                : NoContent();
        }

        [HttpGet]
        [Route("obter/{id}")]
        [Authorize]
        public ActionResult<UsuarioViewModel> Obter(long id)
        {
            _logger.LogDebug("Obter");
            var resultado = _servico.ObterPorId(id);

            _logger.LogDebug($"Obter usuário com sucesso? {resultado != null}");

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<UsuarioViewModel>(resultado))
                : NoContent();
        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public ActionResult<UsuarioViewModel> Cadastrar(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            _logger.LogDebug("Cadastrar");
            var resultado = _servico.Cadastrar(usuario);

            _logger.LogDebug($"Cadastrar: {resultado} usuário cadastrado");

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<UsuarioViewModel>(resultado))
                : NoContent();
        }

        [HttpPost]
        [Route("validarCadastro")]
        [AllowAnonymous]
        public ActionResult<object> ValidarCadastro(Guid cadastro)
        {
            _logger.LogDebug("ValidarCadastro");
            var resultado = _servico.ValidarCadastro(cadastro);

            _logger.LogDebug($"ValidarCadastro usuário validado? {resultado}");

            return (ActionResult)Ok(new { usuarioValidad = resultado });
        }

        [HttpPost]
        [Route("alterar")]
        [Authorize(Roles = "aluno,professor,administrador")]
        public ActionResult<UsuarioViewModel> Alterar(UsuarioDTO usuarioDto)
        {
            var exampleEntity = _mapper.Map<Usuario>(usuarioDto);

            _logger.LogDebug("Alterar");
            var resultado = _servico.Alterar(exampleEntity);

            _logger.LogDebug($"Alterado com sucesso? {resultado != null}");

            return resultado != null
                ? (ActionResult)Ok(_mapper.Map<UsuarioViewModel>(resultado))
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
