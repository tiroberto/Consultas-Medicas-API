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
    [Authorize]
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

        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appUsuario.ObterTodos();

        [HttpGet("obter-por-tipo-usuario")]
        public NotificationResult ObterPorTipoUsuario(int tipoUsuarioId) => appUsuario.ObterPorTipoUsuario(tipoUsuarioId);

        [HttpGet("obter-por-id")]        
        public NotificationResult ObterPorId(int usuarioId) => appUsuario.ObterPorId(usuarioId);
                
        [HttpPost("adicionar")]
        public NotificationResult Adicionar(UsuarioDTO usuarioDTO) => appUsuario.Adicionar(usuarioDTO);

        [HttpPut("atualizar")]
        public NotificationResult Atualizar(UsuarioDTO usuarioDTO) => appUsuario.Atualizar(usuarioDTO);

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
