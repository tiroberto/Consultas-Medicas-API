using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TipoUsuario
    {
        public int TipoUsuarioId { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public IEnumerable<Usuario>? Usuarios { get; set; }

        public void ValidarCampos(TipoUsuario tipoUsuario, NotificationResult notificationResult)
        {
            if (tipoUsuario == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if (string.IsNullOrWhiteSpace(tipoUsuario.Nome))
                    notificationResult.Add(new NotificationError("Nome é obrigatório!"));                
        }
    }
}
