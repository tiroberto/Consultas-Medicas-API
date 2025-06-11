using Comum.NotificationPattern;
using Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Aplicacao
{
    public interface IEspecialidadeAplicacao
    {
        public NotificationResult Adicionar(EspecialidadeDTO especialidadeDTO);
        public NotificationResult Atualizar(EspecialidadeDTO especialidadeDTO);
        public NotificationResult ObterPorId(int id);
        public NotificationResult ObterTodos();
        public NotificationResult ExcluirPorId(int id);
    }
}
