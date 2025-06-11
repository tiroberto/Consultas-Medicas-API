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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private Contexto _contexto;

        public UsuarioRepositorio()
        {
            _contexto = new Contexto();
        }

        public Usuario Adicionar(Usuario usuario)
        {
            usuario.TipoUsuario = _contexto.TiposUsuarios
                .First(x => x.TipoUsuarioId == usuario.TipoUsuario.TipoUsuarioId);
            _contexto.Usuarios.Add(usuario);
            SaveChanges();

            return usuario;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            var usuarioExistente = _contexto.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.SenhaHash = usuario.SenhaHash;
                usuarioExistente.TipoUsuario = _contexto.TiposUsuarios
                    .First(x => x.TipoUsuarioId == usuario.TipoUsuario.TipoUsuarioId);
                
                _contexto.Usuarios.Update(usuarioExistente);
                SaveChanges();
                return usuarioExistente;
            }
            return usuarioExistente;
        }

        public bool ExcluirPorId(int id)
        {
            var usuario = _contexto.Usuarios.Find(id);
            if (usuario != null)
            {
                _contexto.Usuarios.Remove(usuario);
                SaveChanges();
                return true;
            }
            
            return false;
        }

        public Usuario ObterPorId(int id)
        {
            var usuario = _contexto.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefault(u => u.UsuarioId == id);
            
            return usuario;
        }

        public List<Usuario> ObterTodos()
        {
            return _contexto.Usuarios
                .Include(u => u.TipoUsuario)
                .ToList();
        }

        public Usuario ObterPorEmail(string email)
        {
            var usuario = _contexto.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefault(u => u.Email == email);
            
            return usuario;
        }

        public List<Usuario> ObterPorTipoUsuario(int tipoUsuarioId)
        {
            return _contexto.Usuarios
                .Include(u => u.TipoUsuario)
                .Where(u => u.TipoUsuario.TipoUsuarioId == tipoUsuarioId)
                .ToList();
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
