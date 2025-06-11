
using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface IStatusConsultaRepositorio
    {
        public StatusConsulta Adicionar(StatusConsulta status);
        public StatusConsulta Atualizar(StatusConsulta status);
        public StatusConsulta ObterPorId(int statusId);
        public StatusConsulta ObterPorNome(string nome);
        public List<StatusConsulta> ObterTodos();
        public bool ExcluirPorId(int statusId);
        public void SaveChanges();
    }
}
