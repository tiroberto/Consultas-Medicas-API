using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comum.NotificationPattern;
using Dominio;
using Dominio.Entidades;

namespace Dominio.Interfaces.Aplicacao
{
    public interface ITipoUsuarioAplicacao
    {
        public NotificationResult Salvar(TipoUsuario tipoUsuario);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);    }
}
