using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Repositorios
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
        private Contexto _contexto;

        public PacienteRepositorio()
        {
            _contexto = new Contexto();
        }

        public Paciente Adicionar(Paciente paciente)
        {
            _contexto.Pacientes.Add(paciente);
            SaveChanges();
            return paciente;
        }

        public Paciente Atualizar(Paciente paciente)
        {
            var pacienteExistente = _contexto.Pacientes
                .FirstOrDefault(p => p.PacienteId == paciente.PacienteId);

            if(pacienteExistente != null)
            {
                pacienteExistente.Nome = paciente.Nome;
                pacienteExistente.CPF = paciente.CPF;
                pacienteExistente.DataNascimento = paciente.DataNascimento;
                pacienteExistente.Telefone = paciente.Telefone;
                pacienteExistente.Email = paciente.Email;

                _contexto.Pacientes.Update(pacienteExistente);
                SaveChanges();
                return paciente;
            }
            return paciente;
        }

        public Paciente ObterPorId(int pacienteId)
        {
            var paciente = _contexto.Pacientes
                .FirstOrDefault(p => p.PacienteId == pacienteId);
            return paciente;
        }

        public Paciente ObterPorCPF(string cpf)
        {
            var paciente = _contexto.Pacientes
                .FirstOrDefault(p => p.CPF == cpf);
            return paciente;
        }

        public Paciente ObterPorEmail(string email)
        {
            var paciente = _contexto.Pacientes
                .FirstOrDefault(p => p.Email == email);
            return paciente;
        }

        public Paciente ObterPorTelefone(string telefone)
        {
            var paciente = _contexto.Pacientes
                .FirstOrDefault(p => p.Telefone == telefone);
            return paciente;
        }

        public List<Paciente> ObterTodos()
        {
            return _contexto.Pacientes
                .ToList();
        }

        public bool ExcluirPorId(int pacienteId)
        {
            var paciente = _contexto.Pacientes
                .Include(p => p.Consultas)
                .FirstOrDefault(p => p.PacienteId == pacienteId);
            if (paciente != null)
            {
                _contexto.Consultas.RemoveRange(paciente.Consultas);
                _contexto.Pacientes.Remove(paciente);
                SaveChanges();
                return true;                
            }
            return false;
        }

        public bool VerificarExistenciaPaciente(Paciente paciente)
        {
            var pacienteExistente = _contexto.Pacientes
                .FirstOrDefault(p => p.Telefone == paciente.Telefone || p.Email == paciente.Email || p.CPF == paciente.CPF);
            if (pacienteExistente != null)
                return true;
            return false;
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
