using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comum.NotificationPattern;
using Dominio.DTOs;

namespace Dominio.Interfaces.Aplicacao
{
    public interface IStatusConsultaAplicacao
    {
        public NotificationResult Salvar(StatusConsultaDTO statusConsultaDTO);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);
    }
}
