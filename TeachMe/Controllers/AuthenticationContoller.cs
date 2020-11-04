using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeachMe.API.Models.ViewModel;
using TeachMe.Authorization;
using TeachMe.Service.Services.Interfaces;

namespace TeachMe.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioServico _servico;
        private readonly IMapper _mapper;
        public AuthenticationContoller(IMapper mapper, ILogger<UsuarioController> logger, IUsuarioServico servico)
        {
            _mapper = mapper;
            _logger = logger;
            _servico = servico;
        }

        /// <summary>
        /// Login na plataforma para obter token de autorização
        /// </summary>
        /// <param name="email">Email cadastrado</param>
        /// <param name="senha">Senha cadastrada</param>
        /// <returns>Informações do usuário e token de autenticação</returns>
        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<UsuarioViewModel> Login([FromHeader] string email, [FromHeader] string senha)
        {
            _logger.LogDebug("Authenticate");

            var usuario = _servico.Login(email, senha);

            if (usuario == null)
            {
                return new NoContentResult();
            }

            var usuarioAutenticado = TokenHandler.GenerateToken(usuario, _mapper);

            _logger.LogDebug("Usu�rio autenticado");
            return usuarioAutenticado;
        }

        /// <summary>
        /// Testar se o token é válido
        /// </summary>
        /// <returns>String</returns>
        [HttpGet]
        [Route("validateToken")]
        [Authorize]
        public ActionResult<string> IsTokenValide() => "Valid Token!";
    }
}
