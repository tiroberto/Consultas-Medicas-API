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
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoAplicacao appMedico;

        public MedicoController(IMedicoAplicacao medicoAplicacao)
        {
            appMedico = medicoAplicacao;
        }

        
        [HttpGet("obter-todos")]
        public NotificationResult ObterTodos() => appMedico.ObterTodos();

        
        [HttpGet("obter-por-crm")]
        public NotificationResult ObterPorCRM(string crm) => appMedico.ObterPorCRM(crm);

        
        [HttpGet("obter-por-email")]
        public NotificationResult ObterPorEmail(string email) => appMedico.ObterPorEmail(email);

        
        [HttpGet("obter-por-telefone")]
        public NotificationResult ObterPorTelefone(string telefone) => appMedico.ObterPorTelefone(telefone);

        
        [HttpGet("obter-por-id")]
        public NotificationResult ObterPorId(int medicoId) => appMedico.ObterPorId(medicoId);

        
        [HttpGet("obter-por-especialidade")]
        public NotificationResult ObterPorEspecialidade(int especialidadeId) => appMedico.ObterPorEspecialidade(especialidadeId);

        
        [HttpPost("adicionar")]
        public NotificationResult Adicionar(MedicoDTO medicoDTO) => appMedico.Adicionar(medicoDTO);

        [HttpPut("atualizar")]
        public NotificationResult Atualizar(MedicoDTO medicoDTO) => appMedico.Atualizar(medicoDTO);


        [HttpDelete("excluir-por-id")]
        public NotificationResult ExcluirPorId(int medicoId) => appMedico.ExcluirPorId(medicoId);

    }
}
