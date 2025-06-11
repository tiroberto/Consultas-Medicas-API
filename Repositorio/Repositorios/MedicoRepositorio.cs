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
    public class MedicoRepositorio : IMedicoRepositorio
    {
        private Contexto _contexto;

        public MedicoRepositorio() 
        {
            _contexto = new Contexto();
        }

        public Medico Adicionar(Medico medico)
        {
            medico.Especialidade = _contexto.Especialidades
                .First(e => e.EspecialidadeId == medico.Especialidade.EspecialidadeId);
            _contexto.Medicos.Add(medico);
            SaveChanges();
            return medico;
        }

        public Medico Atualizar(Medico medico)
        {
            var medicoExistente = _contexto.Medicos
                .Include(e => e.Especialidade)
                .FirstOrDefault(m => m.MedicoId == medico.MedicoId);
            if(medicoExistente != null)
            {
                medicoExistente.Nome = medico.Nome;
                medicoExistente.CRM = medico.CRM;
                medicoExistente.Email = medico.Email;
                medicoExistente.Telefone = medico.Telefone;
                medicoExistente.Especialidade = _contexto.Especialidades
                    .First(e => e.EspecialidadeId == medico.Especialidade.EspecialidadeId);
                _contexto.Medicos.Update(medicoExistente);
                SaveChanges();
                return medico;
            }
            return medico;
        }

        public bool ExcluirPorId(int id)
        {
            var medicoExistente = _contexto.Medicos
                .Include(m => m.Consultas)
                .FirstOrDefault(m => m.MedicoId == id);
            if(medicoExistente != null)
            {
                _contexto.Consultas.RemoveRange(medicoExistente.Consultas);
                _contexto.Medicos.Remove(medicoExistente);
                SaveChanges();
                return true;
            }
            
            return false;
        }

        public Medico ObterPorId(int medicoId)
        {
            var medicoExistente = _contexto.Medicos
                .Include(e => e.Especialidade)
                .FirstOrDefault(m => m.MedicoId == medicoId);
            return medicoExistente;                
        }

        public List<Medico> ObterPorEspecialidade(int especialidadeId)
        {
            var medicos = _contexto.Medicos
                .Include(e => e.Especialidade)
                .Where(e => e.Especialidade.EspecialidadeId == especialidadeId)
                .ToList();
            return medicos;
        }

        public Medico ObterPorEmail(string email)
        {
            var medicoExistente = _contexto.Medicos
                .Include(e => e.Especialidade)
                .FirstOrDefault(m => m.Email == email);
            return medicoExistente;
        }

        public Medico ObterPorTelefone(string telefone)
        {
            var medicoExistente = _contexto.Medicos
                .Include(e => e.Especialidade)
                .FirstOrDefault(m => m.Telefone == telefone);
            return medicoExistente;
        }

        public Medico ObterPorCRM(string crm)
        {
            var medicoExistente = _contexto.Medicos
                .Include(e => e.Especialidade)
                .FirstOrDefault(m => m.CRM == crm);
            return medicoExistente;
        }

        public bool VerificarExistenciaMedico(Medico medico)
        {
            var medicoExistente = _contexto.Medicos
                .FirstOrDefault(m => m.Telefone == medico.Telefone ||  m.CRM == medico.CRM || m.Email == medico.Email);
            if(medicoExistente != null)
                return true;
            return false;
        }

        public List<Medico> ObterTodos()
        {
            return _contexto.Medicos
                .Include(e => e.Especialidade)
                .ToList();
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
