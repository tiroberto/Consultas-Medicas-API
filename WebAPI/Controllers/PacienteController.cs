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
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteAplicacao appPaciente;

        public PacienteController(IPacienteAplicacao pacienteAplicacao)
        {
            appPaciente = pacienteAplicacao;
        }

        
        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appPaciente.ObterTodos();

        
        [HttpGet("obter-por-cpf")]
        public NotificationResult ObterPorCPF(string cpf) => appPaciente.ObterPorCPF(cpf);

        
        [HttpGet("obter-por-email")]
        public NotificationResult ObterPorEmail(string email) => appPaciente.ObterPorEmail(email);

        
        [HttpGet("obter-por-telefone")]
        public NotificationResult ObterPorTelefone(string telefone) => appPaciente.ObterPorTelefone(telefone);

        
        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int pacienteId) => appPaciente.ObterPorId(pacienteId);

        
        [HttpPost("salvar")]
        public NotificationResult Salvar(PacienteDTO pacienteDTO) => appPaciente.Salvar(pacienteDTO);

        
        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int pacienteId) => appPaciente.ExcluirPorId(pacienteId);
    }
}
