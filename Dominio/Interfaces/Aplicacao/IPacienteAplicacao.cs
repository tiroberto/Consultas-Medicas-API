using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comum;
using Comum.NotificationPattern;
using Dominio.DTOs;

namespace Dominio.Interfaces.Aplicacao
{
    public interface IPacienteAplicacao
    {
        public NotificationResult Adicionar(PacienteDTO pacienteDTO);
        public NotificationResult Atualizar(PacienteDTO pacienteDTO);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterPorCPF(string cpf);
        public NotificationResult ObterPorEmail(string email);
        public NotificationResult ObterPorTelefone(string telefone);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);
    }
}
