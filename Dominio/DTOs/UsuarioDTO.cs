using Comum.NotificationPattern;
using Comum;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace Dominio.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
