using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class ConsultaRepositorio : IConsultaRepositorio
    {
        private Contexto _contexto;

        public ConsultaRepositorio()
        {
            _contexto = new Contexto();
        }

        public Consulta Adicionar(Consulta consulta)
        {
            consulta.Medico = _contexto.Medicos
                .Include(e => e.Especialidade)
                .First(m => m.MedicoId == consulta.Medico.MedicoId);
            consulta.Paciente = _contexto.Pacientes
                .First(p => p.PacienteId == consulta.Paciente.PacienteId);
            consulta.StatusConsulta = _contexto.StatusConsulta
                .First(s => s.StatusConsultaId == consulta.StatusConsulta.StatusConsultaId); // padrão: id=1, nome=agendada
            _contexto.Consultas.Add(consulta);
            SaveChanges();
            return consulta;
        }

        public Consulta Atualizar(Consulta consulta)
        {
            var consultaExistente = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .FirstOrDefault(c => c.ConsultaId == consulta.ConsultaId);
            if(consultaExistente != null)
            {
                consultaExistente.Paciente = _contexto.Pacientes
                    .First(p => p.PacienteId == consulta.Paciente.PacienteId); ;
                consultaExistente.Medico = _contexto.Medicos
                    .Include(e => e.Especialidade)
                    .First(m => m.MedicoId == consulta.Medico.MedicoId); ;
                consultaExistente.StatusConsulta = _contexto.StatusConsulta
                    .First(s => s.StatusConsultaId == consulta.StatusConsulta.StatusConsultaId);
                consultaExistente.Observacoes = consulta.Observacoes;
                consultaExistente.DataHoraFim = consulta.DataHoraFim;
                consultaExistente.DataHoraInicio = consulta.DataHoraInicio;
            
                _contexto.Consultas.Update(consultaExistente);
                SaveChanges();
                return consultaExistente;                
            }

            return consultaExistente;
        }

        public bool ExcluirPorId(int consultaId)
        {
            var consultaExistente = _contexto.Consultas.Find(consultaId);
            if(consultaExistente != null)
            {
                _contexto.Consultas.Remove(consultaExistente);
                SaveChanges();
                return true;                
            }

            return false;
        }

        public Consulta ObterPorId(int consultaId)
        {
            var consultaExistente = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .FirstOrDefault(c => c.ConsultaId == consultaId);
            return consultaExistente;
        }

        public List<Consulta> ObterTodos()
        {
            return _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .ToList();
        }

        public List<Consulta> ObterPorPaciente(int pacienteId)
        {
            var consultas = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .Where(c => c.Paciente.PacienteId == pacienteId)
                .ToList();
            return consultas;
        }

        public List<Consulta> ObterPorMedico(int medicoId)
        {
            var consultas = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .Where(c => c.Medico.MedicoId == medicoId)
                .ToList();
            return consultas;
        }

        public List<Consulta> ObterPorData(DateTime data)
        {
            var consultas = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .Where(c => c.DataHoraInicio.Date == data.Date)
                .ToList();
            return consultas;
        }

        public List<Consulta> ObterPorStatusConsulta(int statusId)
        {
            var consultas = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude (e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .Where(c => c.StatusConsulta.StatusConsultaId == statusId)
                .ToList();
            return consultas;
        }

        public List<Consulta> ObterPorIntervaloDataHora(DateTime inicio, DateTime fim)
        {
            var consultas = _contexto.Consultas
                .Include(m => m.Medico)
                .ThenInclude(e => e.Especialidade)
                .Include(p => p.Paciente)
                .Include(s => s.StatusConsulta)
                .Where(c => c.DataHoraInicio >= inicio && c.DataHoraFim <= fim)
                .ToList();
            return consultas;
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
