using Comum.NotificationPattern;
using Comum;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public void validarCampos(Usuario usuario, NotificationResult notificationResult)
        {            
            if (usuario == null)
                notificationResult.Add(new NotificationError("Entidade nula!"));
            else if (string.IsNullOrWhiteSpace(usuario.Nome))
                notificationResult.Add(new NotificationError("Nome é obrigatório!"));
            else if (string.IsNullOrWhiteSpace(usuario.Email))
                notificationResult.Add(new NotificationError("E-mail é obrigatório!"));
            else if (!FuncoesComuns.ValidarEmail(usuario.Email))            
                notificationResult.Add(new NotificationError("E-mail inválido!"));
            else if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
                notificationResult.Add(new NotificationError("Senha é obrigatória!"));
        }

        public bool VerificarSenha(string senhaDigitada, string senhaHashArmazenada)
        {
            var _passwordHasher = new PasswordHasher<Usuario>();
            var result = _passwordHasher.VerifyHashedPassword(null, senhaHashArmazenada, senhaDigitada);
            return result == PasswordVerificationResult.Success;
        }

        public string GerarHashSenha(string senha)
        {
            var _passwordHasher = new PasswordHasher<Usuario>();
            return _passwordHasher.HashPassword(null, senha);
        }
    }
}
