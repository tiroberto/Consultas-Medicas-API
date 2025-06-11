using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class StatusConsulta
    {
        public int StatusConsultaId { get; set; }
        public string Nome { get; set; }        

        public IEnumerable<Consulta> Consultas { get; set; }

        public void ValidarCampos (StatusConsulta status, NotificationResult notificationResult)
        {
            if(status == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if (string.IsNullOrEmpty(status.Nome))
                    notificationResult.Add(new NotificationError("Nome é obrigatório!"));            
        }
    }
}
