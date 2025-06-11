using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusConsultaController : ControllerBase
    {
        private readonly IStatusConsultaAplicacao appStatusConsulta;

        public StatusConsultaController(IStatusConsultaAplicacao StatusConsultaAplicacao)
        {
            appStatusConsulta = StatusConsultaAplicacao;
        }

        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appStatusConsulta.ObterTodos();

        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int id) => appStatusConsulta.ObterPorId(id);

        [HttpPost("salvar")]
        public NotificationResult Salvar(StatusConsultaDTO statusConsultaDTO) => appStatusConsulta.Salvar(statusConsultaDTO);

        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int id) => appStatusConsulta.ExcluirPorId(id);
    }
}
