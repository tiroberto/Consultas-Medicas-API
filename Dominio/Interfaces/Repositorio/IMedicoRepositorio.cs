using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface IMedicoRepositorio
    {
        public Medico Adicionar(Medico medico);
        public Medico Atualizar(Medico medico);
        public bool ExcluirPorId(int id);
        public Medico ObterPorId(int medicoId);
        public List<Medico> ObterPorEspecialidade(int especialidadeId);
        public Medico ObterPorEmail(string email);
        public Medico ObterPorTelefone(string telefone);
        public Medico ObterPorCRM(string crm);
        public bool VerificarExistenciaMedico(Medico medico);
        public List<Medico> ObterTodos();
        public void SaveChanges();
    }
}
