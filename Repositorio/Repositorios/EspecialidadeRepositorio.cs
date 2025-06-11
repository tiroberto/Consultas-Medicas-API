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
    public class EspecialidadeRepositorio : IEspecialidadeRepositorio
    {
        private Contexto _contexto;

        public EspecialidadeRepositorio()
        {
            _contexto = new Contexto();
        }

        public Especialidade Adicionar(Especialidade especialidade)
        {
            _contexto.Especialidades.Add(especialidade);
            SaveChanges();
            return especialidade;
        }

        public Especialidade Atualizar(Especialidade especialidade)
        {
            var especialidadeExistente = _contexto.Especialidades
                .FirstOrDefault(e => e.EspecialidadeId == especialidade.EspecialidadeId);
            if (especialidadeExistente != null) 
            {
                especialidadeExistente.Nome = especialidade.Nome;
                _contexto.Especialidades.Update(especialidadeExistente);
                SaveChanges();
                return especialidadeExistente;            
            }

            return especialidadeExistente;
        }

        public Especialidade ObterPorId(int especialidadeId)
        {
            var especialidade = _contexto.Especialidades
                .FirstOrDefault(e => e.EspecialidadeId == especialidadeId);

            return especialidade;
        }

        public Especialidade ObterPorNome(string nome)
        {
            var especialidade = _contexto.Especialidades
                .FirstOrDefault(e => e.Nome == nome);

            return especialidade;
        }

        public List<Especialidade> ObterTodos()
        {
            return _contexto.Especialidades
                .ToList();
        }

        public bool ExcluirPorId(int especialidadeId)
        {
            var especialidade = _contexto.Especialidades
                .Include(e => e.Medicos)
                .FirstOrDefault(e => e.EspecialidadeId == especialidadeId);
            if (especialidade != null)
            {
                _contexto.Medicos.RemoveRange(especialidade.Medicos);
                _contexto.Especialidades.Remove(especialidade);
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
