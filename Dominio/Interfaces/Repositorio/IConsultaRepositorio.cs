using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorio
{
    public interface IConsultaRepositorio
    {
        public Consulta Adicionar(Consulta consulta);
        public Consulta Atualizar(Consulta consulta);
        public bool ExcluirPorId(int consultaId);
        public Consulta ObterPorId(int consultaId);
        public List<Consulta> ObterTodos();
        public List<Consulta> ObterPorPaciente(int pacienteId);
        public List<Consulta> ObterPorMedico(int medicoId);
        public List<Consulta> ObterPorData(DateTime data);
        public List<Consulta> ObterPorStatusConsulta(int statusId);
        public List<Consulta> ObterPorIntervaloDataHora(DateTime inicio, DateTime fim);
        public void SaveChanges();
    }
}
