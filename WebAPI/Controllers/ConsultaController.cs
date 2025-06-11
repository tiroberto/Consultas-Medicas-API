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
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaAplicacao appConsulta;

        public ConsultaController(IConsultaAplicacao consultaAplicacao)
        {
            appConsulta = consultaAplicacao;
        }

        
        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appConsulta.ObterTodos();

        
        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int consultaId) => appConsulta.ObterPorId(consultaId);

        
        [HttpPost("adicionar")]
        public NotificationResult Adicionar(ConsultaDTO consultaDTO) => appConsulta.Adicionar(consultaDTO);

        [HttpPut("atualizar")]
        public NotificationResult Atualizar(ConsultaDTO consultaDTO) => appConsulta.Atualizar(consultaDTO);


        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int consultaId) => appConsulta.ExcluirPorId(consultaId);

        
        [HttpGet("obter-por-status-consulta")]
        public NotificationResult ObterPorStatusConsulta(int statusId) => appConsulta.ObterPorStatusConsulta(statusId);

        
        [HttpGet("obter-por-paciente")]
        public NotificationResult ObterPorPaciente(int pacienteId) => appConsulta.ObterPorPaciente(pacienteId);

        
        [HttpGet("obter-por-medico")]
        public NotificationResult ObterPorMedico(int medicoId) => appConsulta.ObterPorMedico(medicoId);

        
        [HttpGet("obter-por-data")]
        public NotificationResult ObterPorData(DateTime data) => appConsulta.ObterPorData(data);

        
        [HttpGet("obter-por-invervalo-data-hora")]
        public NotificationResult ObterPorIntervaloDataHora(DateTime dataHoraInicio, DateTime dataHoraFim) => appConsulta.ObterPorIntervaloDataHora(dataHoraInicio, dataHoraFim);
    }
}
