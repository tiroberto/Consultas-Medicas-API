using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Medico
    {
        public int MedicoId { get; set; }
        public string Nome { get; set; } 
        public string CRM { get; set; } 
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Especialidade Especialidade { get; set; } 

        public ICollection<Consulta> Consultas { get; set; } 

        public void ValidarCampos(Medico medico, NotificationResult notificationResult)
        {
            if (medico == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if (string.IsNullOrEmpty(medico.Nome))
                notificationResult.Add(new NotificationError("Nome é obrigatório!"));
            else if (string.IsNullOrEmpty(medico.CRM))
                notificationResult.Add(new NotificationError("CRM é obrigatório!"));
            else if (string.IsNullOrEmpty(medico.Email))
                notificationResult.Add(new NotificationError("E-mail é obrigatório!"));
            else if (string.IsNullOrEmpty(medico.Telefone))
                notificationResult.Add(new NotificationError("Telefone é obrigatório!"));
            else if (medico.Especialidade.EspecialidadeId == 0 || medico.Especialidade == null)
                notificationResult.Add(new NotificationError("Especialidade é obrigatória!"));
            else if (!FuncoesComuns.ValidarEmail(medico.Email))
                notificationResult.Add(new NotificationError("E-mail inválido!"));
            else if (!FuncoesComuns.ValidarTelefone(medico.Telefone))
                notificationResult.Add(new NotificationError("Telefone inválido!"));
        }
    }
}
