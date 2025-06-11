using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeAplicacao appEspecialidade;

        public EspecialidadeController(IEspecialidadeAplicacao EspecialidadeAplicacao)
        {
            appEspecialidade = EspecialidadeAplicacao;
        }

        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appEspecialidade.ObterTodos();

        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int id) => appEspecialidade.ObterPorId(id);

        [HttpPost("salvar")]
        public NotificationResult Salvar(EspecialidadeDTO especialidadeDTO) => appEspecialidade.Salvar(especialidadeDTO);

        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int especialidadeId) => appEspecialidade.ExcluirPorId(especialidadeId);        
    }
}
