using Dominio.DTOs;
using Comum.NotificationPattern;
using Comum;


namespace Dominio.Interfaces.Aplicacao
{
    public interface IUsuarioAplicacao
    {
        public NotificationResult Salvar(UsuarioDTO usuarioDTO);
        public NotificationResult VerificarLogin(string email, string senhaDigitada);
        public NotificationResult ObterTodos();
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterPorTipoUsuario(int tipoUsuarioId);
        public NotificationResult ExcluirPorId(int id);
    }        
}
