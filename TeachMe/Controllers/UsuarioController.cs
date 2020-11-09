using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;
using TeachMe.Service.Services.Interfaces;

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

        /// <summary>
        /// Obter todos os usuários presentes na base de dados (Necessita de token de autenticação)
        /// </summary>
        /// <returns>Lista de Usuários cadastrados</returns>
        [HttpGet]
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

        /// <summary>
        /// Obter usuário utilizando seu id (Necessita de token de autenticação)
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Usuário referente ao id fornecido</returns>
        [HttpGet]
        [Route("porId")]
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

        /// <summary>
        /// Cadastrar usuário dado os parametros fornecidos (Permite acesso sem autenticação)
        /// </summary>
        /// <param name="usuarioDto">Informações do Usuário</param>
        /// <returns>Usuário cadastrado</returns>
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

        /// <summary>
        /// Valida cadastro a partir do id da url enviada para o e-mail (Permite acesso sem autenticação)
        /// </summary>
        /// <param name="cadastro">Id presente na url enviada por e-mail</param>
        /// <returns>usuarioValidado: boolean</returns>
        [HttpGet]
        [Route("validarCadastro")]
        [AllowAnonymous]
        public ActionResult<object> ValidarCadastro(Guid cadastro)
        {
            _logger.LogDebug("ValidarCadastro");
            var resultado = _servico.ValidarCadastro(cadastro);

            _logger.LogDebug($"ValidarCadastro usuário validado? {resultado}");

            return (ActionResult)Ok(new { usuarioValidado = resultado });
        }

        /// <summary>
        /// Altera informações do usuário informado (Necessita de token de autenticação)
        /// </summary>
        /// <param name="usuarioDto">Informações do usuário para salvar</param>
        /// <returns>Estado do usuário após ser alterado</returns>
        [HttpPost]
        [Route("alterar")]
        [Authorize]
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

        /// <summary>
        /// Exclui o usuário identificado pelo id fornecido (Acesso só para administrador)
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>UsuarioDeletado: boolean</returns>
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
