using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface IEspecialidadeRepositorio 
    {
        public Especialidade Adicionar(Especialidade especialidade);
        public Especialidade Atualizar(Especialidade especialidade);
        public Especialidade ObterPorId(int especialidadeId);
        public Especialidade ObterPorNome(string nome);
        public List<Especialidade> ObterTodos();
        public bool ExcluirPorId(int especialidadeId);
        public void SaveChanges();
    }
}
