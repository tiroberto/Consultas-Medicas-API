using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface IPacienteRepositorio
    {
        public Paciente Adicionar(Paciente paciente);
        public Paciente Atualizar(Paciente paciente);
        public Paciente ObterPorId(int pacienteId);
        public Paciente ObterPorCPF(string cpf);
        public Paciente ObterPorEmail(string email);
        public Paciente ObterPorTelefone(string telefone);
        public List<Paciente> ObterTodos();
        public bool VerificarExistenciaPaciente(Paciente paciente);
        public bool ExcluirPorId(int pacienteId);
        public void SaveChanges();
    }
}
