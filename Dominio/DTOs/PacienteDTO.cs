using Comum;
using Comum.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTOs
{
    public class PacienteDTO
    {
        public int PacienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
