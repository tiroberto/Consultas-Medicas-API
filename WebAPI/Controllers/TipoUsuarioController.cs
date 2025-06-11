using Dominio.Interfaces.Aplicacao;
using Comum.NotificationPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entidades;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuarioAplicacao appTipoUsuario;

        public TipoUsuarioController(ITipoUsuarioAplicacao TipoUsuarioAplicacao)
        {
            appTipoUsuario = TipoUsuarioAplicacao;
        }

        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appTipoUsuario.ObterTodos();

        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int id) => appTipoUsuario.ObterPorId(id);

        /*[HttpPost("salvar")]
        public NotificationResult Salvar(TipoUsuario tipoUsuario) => appTipoUsuario.Salvar(tipoUsuario);*/

        /*[HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int id) => appTipoUsuario.ExcluirPorId(id);*/
    }
}
