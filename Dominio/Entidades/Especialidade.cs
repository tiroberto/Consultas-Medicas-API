using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Especialidade 
    {
        public int EspecialidadeId { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Medico> Medicos { get; set; }

        public void ValidarCampos(Especialidade especialidade, NotificationResult notificationResult)
        {
            if (especialidade == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if (string.IsNullOrEmpty(especialidade.Nome))
                notificationResult.Add(new NotificationError("Nome é obrigatório!"));
        }
    }
}
