using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao appUsuario;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            appUsuario = usuarioAplicacao;
        }

        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appUsuario.ObterTodos();

        [HttpGet("obter-por-tipo-usuario")]
        public NotificationResult ObterPorTipoUsuario(int tipoUsuarioId) => appUsuario.ObterPorTipoUsuario(tipoUsuarioId);

        [HttpGet("obter-por-id")]        
        public NotificationResult ObterPorId(int usuarioId) => appUsuario.ObterPorId(usuarioId);

        [HttpPost("salvar")]
        public NotificationResult Salvar(UsuarioDTO usuarioDTO) => appUsuario.Salvar(usuarioDTO);


        [HttpDelete("excluir-por-id")]
        public NotificationResult Excluir(int usuarioId) => appUsuario.ExcluirPorId(usuarioId);

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public dynamic Authenticate(string email, string senha) => appUsuario.VerificarLogin(email, senha);
    }
}
