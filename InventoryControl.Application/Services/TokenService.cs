using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Models.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryControl.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IAcessosService _acessosService;
        private readonly IUsuarioService _usuarioService;

        public TokenService(IAcessosService acessosService,
            IUsuarioService usuarioService)
        {
            this._acessosService = acessosService;
            this._usuarioService = usuarioService;
        }

        public async Task<string> GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var roles = await _usuarioService.FindAcessosByPerfilUsuarioId(user.PerfilUsuarioId.GetValueOrDefault());

            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Nome));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
