using Comum.NotificationPattern;
using Dominio.Entidades;
using Dominio.Interfaces.Aplicacao;
using Dominio.Interfaces.Repositorio;
using Microsoft.IdentityModel.Tokens;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class TipoUsuarioAplicacao : ITipoUsuarioAplicacao
    {
        private readonly ITipoUsuarioRepositorio _tipoUsuarioRepositorio;
        private readonly TipoUsuario _tipoUsuarioDominio;

        public TipoUsuarioAplicacao(ITipoUsuarioRepositorio tipoUsuarioRepositorio)
        {
            _tipoUsuarioRepositorio = tipoUsuarioRepositorio;
            _tipoUsuarioDominio = new TipoUsuario();
        }

        public NotificationResult Salvar(TipoUsuario tipoUsuario)
        {
            var notificationResult = new NotificationResult();
            try
            {
                _tipoUsuarioDominio.ValidarCampos(tipoUsuario, notificationResult);

                if (notificationResult.IsValid)
                {
                    if(tipoUsuario.TipoUsuarioId == 0)
                    {
                        var tipoUsuarioExistente = _tipoUsuarioRepositorio.ObterPorNome(tipoUsuario.Nome);

                        if(tipoUsuarioExistente == null)
                        {
                            tipoUsuario = _tipoUsuarioRepositorio.Adicionar(tipoUsuario);

                            if(tipoUsuario.TipoUsuarioId > 0)
                            {
                                notificationResult.Result = tipoUsuario;
                                notificationResult.Add("Tipo de usuário adicionado com sucesso!");
                                return notificationResult;
                            }
                        }
                        else
                        {
                            notificationResult.Add("Tipo de usuário já existe!");
                            return notificationResult;
                        }
                        
                    }
                    else
                    {
                        tipoUsuario = _tipoUsuarioRepositorio.Atualizar(tipoUsuario);

                        if(tipoUsuario != null)
                        {
                            notificationResult.Result = tipoUsuario;
                            notificationResult.Add("Tipo de usuário atualizado com sucesso!");
                            return notificationResult;
                        }
                    }
                }

                return notificationResult;
            }
            catch (Exception e)
            {
                return notificationResult.Add(new NotificationError(e.Message));
            }
        }

        public NotificationResult ObterPorId(int id)
        {
            var notificationResult = new NotificationResult();
            try
            {
                if(notificationResult.IsValid)
                {
                    var tipoUsuarioExistente = _tipoUsuarioRepositorio.ObterPorId(id);
                    
                    if(tipoUsuarioExistente != null)
                    {
                        notificationResult.Result = tipoUsuarioExistente;
                        notificationResult.Add("Tipo de usuário encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Tipo de usuário não encontrado!");
                        return notificationResult;
                    }
                }

                return notificationResult;
            }
            catch (Exception e)
            {
                return notificationResult.Add(new NotificationError(e.Message));
            }
        }

        public NotificationResult ObterTodos() 
        {
            var notificationResult = new NotificationResult();
            try
            {
                if(notificationResult.IsValid)
                {
                    var listaTipoUsuarios = _tipoUsuarioRepositorio.ObterTodos();

                    if(listaTipoUsuarios.Count > 0)
                    {
                        notificationResult.Result = listaTipoUsuarios;
                        notificationResult.Add("Lista criada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Nenhum tipo de usuário encontrado");
                        return notificationResult;
                    }
                }

                return notificationResult;
            }
            catch (Exception e) 
            {
                return notificationResult.Add(new NotificationError(e.Message));
            }
        }

        public NotificationResult ExcluirPorId(int id)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var resultadoExclusao = _tipoUsuarioRepositorio.ExcluirPorId(id);

                    if (resultadoExclusao)
                    {
                        notificationResult.Add("Excluído com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Erro na exclusão!");
                        return notificationResult;
                    }
                }

                return notificationResult;
            }
            catch (Exception e)
            {
                return notificationResult.Add(new NotificationError(e.Message));
            }
        }
    }
}
