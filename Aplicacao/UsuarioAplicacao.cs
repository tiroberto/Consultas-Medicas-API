using Repositorio.Repositorios;
using Comum.NotificationPattern;
using Microsoft.AspNetCore.Identity;
using Comum;
using Dominio.Entidades;
using Dominio.DTOs;
using Dominio.Interfaces.Aplicacao;
using Dominio.Interfaces.Repositorio;


namespace Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly Usuario _usuarioDominio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioDominio = new Usuario();
        }

        public NotificationResult Salvar (UsuarioDTO usuarioDTO)
        {
            var notificationResult = new NotificationResult();
            
            try
            {
                var user = new Usuario
                {
                    UsuarioId = usuarioDTO.UsuarioId,
                    Nome = usuarioDTO.Nome,
                    Email = usuarioDTO.Email,
                    SenhaHash = usuarioDTO.SenhaHash,
                    TipoUsuario = new TipoUsuario { TipoUsuarioId = usuarioDTO.TipoUsuarioId }
                };
                _usuarioDominio.validarCampos(user, notificationResult);
                if (notificationResult.IsValid)
                {
                    if(user.UsuarioId == 0)
                    {                  
                        
                        var usuarioExistente = _usuarioRepositorio.ObterPorEmail(user.Email);
                        
                        if (usuarioExistente == null) 
                        {
                            user.SenhaHash = _usuarioDominio.GerarHashSenha(user.SenhaHash);
                            user = _usuarioRepositorio.Adicionar(user);

                            if(user.UsuarioId > 0)
                            {
                                notificationResult.Result = user;
                                notificationResult.Add("Usuário cadastrado com sucesso!");
                                return notificationResult;
                            }
                        }
                        else
                        {                            
                            notificationResult.Result = usuarioExistente;
                            notificationResult.Add("Usuário já possui cadastro!");
                            return notificationResult;
                        }
                    }
                    else
                    {
                        user.SenhaHash = _usuarioDominio.GerarHashSenha(user.SenhaHash);
                        user = _usuarioRepositorio.Atualizar(user);

                        if(user != null)
                        {
                            notificationResult.Result = user;
                            notificationResult.Add("Dados do usuário atualizados com sucesso!");
                            return notificationResult;
                        }
                    }
                }
                                
                return notificationResult;
            }
            catch (Exception ex) 
            {
                return notificationResult.Add(new NotificationError(ex.Message));
            }
        }


        //falta implementar autenticação com token
        public NotificationResult VerificarLogin(string email, string senhaDigitada)
        {
            var notificationResult = new NotificationResult();
            try
            {
                if(notificationResult.IsValid)
                {
                    var validacaoEmail = FuncoesComuns.ValidarEmail(email);

                    if (validacaoEmail)
                    {
                        var usuarioExistente = _usuarioRepositorio.ObterPorEmail(email);
                        var verificacaoSenha = _usuarioDominio.VerificarSenha(senhaDigitada, usuarioExistente.SenhaHash);

                        if (usuarioExistente != null && verificacaoSenha)
                        {
                            notificationResult.Result = new { usuario = usuarioExistente, token = "" };
                            notificationResult.Add("Login efetuado com sucesso.");
                            return notificationResult;
                        }
                        else 
                        {
                            notificationResult.Add("Usuário não encontrado.");
                            return notificationResult;
                        }
                    }
                    else
                    {
                        notificationResult.Add("E-mail inválido!");
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
                if (notificationResult.IsValid)
                {
                    var listaUsuarios = _usuarioRepositorio.ObterTodos();

                    if (listaUsuarios.Count > 0)
                    {
                        notificationResult.Result = listaUsuarios;
                        notificationResult.Add("Lista de todos usuários criada.");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Usuários não encontrados.");
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

        public NotificationResult ObterPorId(int id)
        {
            var notificationResult = new NotificationResult();
            try
            {
                if (notificationResult.IsValid) 
                {
                    var usuarioExistente = _usuarioRepositorio.ObterPorId(id);

                    if(usuarioExistente != null)
                    {
                        notificationResult.Result = usuarioExistente;
                        notificationResult.Add("Usuário encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Usuário não existente!");
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

        public NotificationResult ObterPorTipoUsuario(int tipoUsuarioId)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var usuarios = _usuarioRepositorio.ObterPorTipoUsuario(tipoUsuarioId);

                    if (usuarios.Count > 0)
                    {
                        notificationResult.Result = usuarios;
                        notificationResult.Add("Lista de usuários criada!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Usuários não encontrados!");
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
                    var resultadoExclusao = _usuarioRepositorio.ExcluirPorId(id);

                    if(resultadoExclusao)
                    {
                        notificationResult.Add("Usuário excluído com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Usuário não encontrado!");
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
