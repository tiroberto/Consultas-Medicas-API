using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; } 
        public DateOnly DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }        

        public IEnumerable<Consulta> Consultas { get; set; }

        public void ValidarCampos(Paciente paciente, NotificationResult notificationResult)
        {
            if (paciente == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if(string.IsNullOrEmpty(paciente.Nome))
                notificationResult.Add(new NotificationError("Nome é obrigatório!"));
            else if (string.IsNullOrEmpty(paciente.CPF))
                notificationResult.Add(new NotificationError("CPF é obrigatório!"));
            else if (string.IsNullOrEmpty(paciente.Email))
                notificationResult.Add(new NotificationError("E-mail é obrigatório!"));
            else if (string.IsNullOrEmpty(paciente.Telefone))
                notificationResult.Add(new NotificationError("Telefone é obrigatório!"));
            else if (!FuncoesComuns.ValidarEmail(paciente.Email))
                notificationResult.Add(new NotificationError("E-mail inválido!"));
            else if (!FuncoesComuns.ValidarTelefone(paciente.Telefone))
                notificationResult.Add(new NotificationError("Telefone inválido!"));
            else if (!FuncoesComuns.ValidarCPF(paciente.CPF))
                notificationResult.Add(new NotificationError("CPF inválido!"));
        }
    }
}
