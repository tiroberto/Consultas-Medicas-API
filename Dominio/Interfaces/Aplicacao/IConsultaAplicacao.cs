using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comum.NotificationPattern;
using Dominio.DTOs;

namespace Dominio.Interfaces.Aplicacao
{
    public interface IConsultaAplicacao
    {
        public NotificationResult Adicionar(ConsultaDTO consultaDTO);
        public NotificationResult Atualizar(ConsultaDTO consultaDTO);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);
        public NotificationResult ObterPorStatusConsulta(int statusId);
        public NotificationResult ObterPorPaciente(int pacienteId);
        public NotificationResult ObterPorMedico(int medicoId);
        public NotificationResult ObterPorData(DateTime data);
        public NotificationResult ObterPorIntervaloDataHora(DateTime dataHoraInicio, DateTime dataHoraFim);
    }
}
