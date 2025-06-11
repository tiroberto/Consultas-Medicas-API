using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao appUsuario;
        private readonly IConfiguration _config;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao, IConfiguration config)
        {
            appUsuario = usuarioAplicacao;
            _config = config;
        }

//        [Authorize]
        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appUsuario.ObterTodos();

        [Authorize]
        [HttpGet("obter-por-tipo-usuario")]
        public NotificationResult ObterPorTipoUsuario(int tipoUsuarioId) => appUsuario.ObterPorTipoUsuario(tipoUsuarioId);

        [Authorize]
        [HttpGet("obter-por-id")]        
        public NotificationResult ObterPorId(int usuarioId) => appUsuario.ObterPorId(usuarioId);

        [Authorize]
        [AllowAnonymous]
        [HttpPost("salvar")]
        public NotificationResult Salvar(UsuarioDTO usuarioDTO) => appUsuario.Salvar(usuarioDTO);

        [Authorize]
        [HttpDelete("excluir-por-id")]
        public NotificationResult Excluir(int usuarioId) => appUsuario.ExcluirPorId(usuarioId);
        
        [HttpPost("login")]
        [AllowAnonymous]
        public dynamic Authenticate([FromBody]UsuarioDTO usuarioDTO) 
        {
            var usuario = appUsuario.VerificarLogin(usuarioDTO.Email, usuarioDTO.Senha);
            if(usuario.Result != null)
            {
                var token = GerarTokenJWT(usuarioDTO.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GerarTokenJWT(string email)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpireHours"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
