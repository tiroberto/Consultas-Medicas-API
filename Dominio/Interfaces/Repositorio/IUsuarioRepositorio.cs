using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio
    {
        public Usuario Adicionar(Usuario usuario);
        public Usuario Atualizar(Usuario usuario);
        public bool ExcluirPorId(int id);
        public Usuario ObterPorId(int id);
        public List<Usuario> ObterTodos();
        public Usuario ObterPorEmail(string email);
        public List<Usuario> ObterPorTipoUsuario(int tipoUsuarioId);
        public void SaveChanges();
    }
}
