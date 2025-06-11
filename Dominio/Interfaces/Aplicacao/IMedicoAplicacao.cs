using Comum;
using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Aplicacao
{
    public interface IMedicoAplicacao
    {
        public NotificationResult Salvar(MedicoDTO medicoDTO);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterPorCRM(string crm);
        public NotificationResult ObterPorEmail(string email);
        public NotificationResult ObterPorTelefone(string telefone);
        public NotificationResult ObterPorEspecialidade(int especialidadeId);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);
    }
}
