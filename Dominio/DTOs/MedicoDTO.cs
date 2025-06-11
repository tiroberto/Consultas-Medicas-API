using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTOs
{
    public class MedicoDTO
    {
        public int MedicoId { get; set; }
        public string Nome { get; set; } 
        public string CRM { get; set; } 
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int EspecialidadeId { get; set; } 
    }
}
