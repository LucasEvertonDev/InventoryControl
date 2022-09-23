using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace InventoryControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public AccountController(ILogger<AccountController> logger,
            IUsuarioService usuarioService,
            ITokenService tokenService,
            IPasswordHashService passwordHashService)
        {
            _logger = logger;
            this._usuarioService = usuarioService;
            this._tokenService = tokenService;
            this._passwordHashService = passwordHashService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginModel model)
        {
            var user =  await _usuarioService.Login(model.Login, model.Senha);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = await _tokenService.GenerateToken(user);

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<int> CreateUser([FromBody] LoginModel usuario)
        {
            var encrypted = _passwordHashService.EncryptPassword(usuario.Senha);
            var compare = _passwordHashService.EncryptPassword(usuario.Senha);

            var equals =_passwordHashService.VerifyPassword(encrypted.Hash, compare.Hash);

            return Task.FromResult(1);
        }

    }
}