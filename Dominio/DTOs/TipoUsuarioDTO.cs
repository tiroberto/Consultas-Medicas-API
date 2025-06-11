using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.DTOs
{
    public class TipoUsuarioDTO
    {
        public int TipoUsuarioId { get; set; }
        public string Nome { get; set; }
    }
}
