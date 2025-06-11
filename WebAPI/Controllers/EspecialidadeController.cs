using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
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

        
        [HttpPost("adicionar")]
        public NotificationResult Adicionar(EspecialidadeDTO especialidadeDTO) => appEspecialidade.Adicionar(especialidadeDTO);

        [HttpPut("atualizar")]
        public NotificationResult Atualizar(EspecialidadeDTO especialidadeDTO) => appEspecialidade.Atualizar(especialidadeDTO);


        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int especialidadeId) => appEspecialidade.ExcluirPorId(especialidadeId);        
    }
}
