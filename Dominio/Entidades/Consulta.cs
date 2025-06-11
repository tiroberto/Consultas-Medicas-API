using Comum.NotificationPattern;

namespace Dominio.Entidades
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public Paciente Paciente { get; set; } 
        public Medico Medico { get; set; }
        // exmplo: 2025-06-10T10:30:00 (10/06/2025 10:30:00 em pt-br)
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public StatusConsulta StatusConsulta { get; set; } 
        public string? Observacoes { get; set; }

        public void ValidarCampos(Consulta consulta, NotificationResult notificationResult)
        {
            if (consulta == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if(consulta.Medico.MedicoId == 0 || consulta.Medico == null)
                notificationResult.Add(new NotificationError("Médico é obrigatório!"));
            else if (consulta.Paciente.PacienteId == 0 || consulta.Paciente == null)
                notificationResult.Add(new NotificationError("Paciente é obrigatório!"));
            else if (consulta.DataHoraInicio <= DateTime.Now)
                notificationResult.Add(new NotificationError("A data e hora da consulta devem ser no futuro."));
            else if (consulta.DataHoraFim <= consulta.DataHoraInicio)
                notificationResult.Add(new NotificationError("O horário de término deve ser maior que o de início."));
        }
    }
}