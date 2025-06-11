using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface ITipoUsuarioRepositorio
    {
        public TipoUsuario Adicionar(TipoUsuario tipoUsuario);
        public TipoUsuario Atualizar(TipoUsuario tipoUsuario);
        public bool ExcluirPorId(int id);
        public TipoUsuario ObterPorId(int tipoUsuarioId);
        public TipoUsuario ObterPorNome(string nome);
        public List<TipoUsuario> ObterTodos();
        public void SaveChanges();
    }
}
