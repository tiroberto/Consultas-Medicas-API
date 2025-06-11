using Comum.NotificationPattern;

namespace Dominio.DTOs
{
    public class ConsultaDTO
    {
        public int ConsultaId { get; set; }
        public int PacienteId { get; set; } 
        public int MedicoId { get; set; } 
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public int StatusConsultaId { get; set; } 
        public string? Observacoes { get; set; }
    }
}