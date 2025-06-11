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
    public class TipoUsuarioRepositorio : ITipoUsuarioRepositorio
    {
        private Contexto _contexto;

        public TipoUsuarioRepositorio()
        {
            _contexto = new Contexto();
        }

        public TipoUsuario Adicionar(TipoUsuario tipoUsuario)
        {
            _contexto.TiposUsuarios.Add(tipoUsuario);
            SaveChanges();
            return tipoUsuario;
        }

        public TipoUsuario Atualizar(TipoUsuario tipoUsuario)
        {
            var tipoUsuarioExistente = _contexto.TiposUsuarios
                .FirstOrDefault(t => t.TipoUsuarioId == tipoUsuario.TipoUsuarioId);
            if(tipoUsuarioExistente != null)
            {
                tipoUsuarioExistente.Nome = tipoUsuario.Nome;
                _contexto.TiposUsuarios.Update(tipoUsuarioExistente);
                SaveChanges();
                return tipoUsuarioExistente;
            }

            return tipoUsuarioExistente;
        }

        public bool ExcluirPorId(int id)
        {
            var tipoUsuario = _contexto.TiposUsuarios
                .Include(t => t.Usuarios)
                .FirstOrDefault(t => t.TipoUsuarioId == id);
            if (tipoUsuario != null)
            {
                _contexto.Usuarios.RemoveRange(tipoUsuario.Usuarios);
                _contexto.TiposUsuarios.Remove(tipoUsuario);
                SaveChanges();
                return true;
            }
            return false;
        }        

        public TipoUsuario ObterPorId(int tipoUsuarioId)
        {
            var tipoUsuario = _contexto.TiposUsuarios
                .FirstOrDefault(t => t.TipoUsuarioId == tipoUsuarioId);

            return tipoUsuario;
        }       
        
        public TipoUsuario ObterPorNome(string nome)
        {
            var tipoUsuario = _contexto.TiposUsuarios
                .FirstOrDefault(t => t.Nome == nome);

            return tipoUsuario;
        }

        public List<TipoUsuario> ObterTodos()
        {
            return _contexto.TiposUsuarios.ToList();
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
