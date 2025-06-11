using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Repositorios
{
    public class StatusConsultaRepositorio : IStatusConsultaRepositorio
    {
        private Contexto _contexto;

        public StatusConsultaRepositorio()
        {
            _contexto = new Contexto();
        }

        public StatusConsulta Adicionar(StatusConsulta status)
        {
            _contexto.StatusConsulta.Add(status);
            SaveChanges();
            return status;
        }

        public StatusConsulta Atualizar(StatusConsulta status)
        {
            var statusExistente = _contexto.StatusConsulta
                .FirstOrDefault(s => s.StatusConsultaId == status.StatusConsultaId);
            if (statusExistente != null)
            {
                statusExistente.Nome = status.Nome;
                _contexto.StatusConsulta.Update(statusExistente);
                SaveChanges();
                return statusExistente;                
            }

            return statusExistente;
        }

        public StatusConsulta ObterPorId(int statusId)
        {
            var status = _contexto.StatusConsulta
                .FirstOrDefault(s => s.StatusConsultaId == statusId);

            return status;
        }

        public StatusConsulta ObterPorNome(string nome)
        {
            var status = _contexto.StatusConsulta
                .FirstOrDefault(s => s.Nome == nome);

            return status;
        }

        public List<StatusConsulta> ObterTodos()
        {
            return _contexto.StatusConsulta
                .ToList();
        }

        public bool ExcluirPorId (int statusId)
        {
            var status = _contexto.StatusConsulta
                .Include(s => s.Consultas)
                .FirstOrDefault(s => s.StatusConsultaId == statusId);
            if(status != null)
            {
                _contexto.Consultas.RemoveRange(status.Consultas);
                _contexto.StatusConsulta.Remove(status);
                SaveChanges();
                return true;                
            }

            return false;
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
