using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeachMe.Authorization;

namespace TeachMe.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        public AuthenticationContoller(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<User> Authenticate()
        {
            _logger.LogDebug("Authenticate");

            var token = TokenHandler.GenerateToken();
            var userAuthentication = new User { Token = token };

            _logger.LogDebug("Token value: " + token);

            return userAuthentication;
        }

        [HttpGet]
        [Route("validateToken")]
        [Authorize]
        public ActionResult<string> IsTokenValide() => "Valid Token!";
    }
}
