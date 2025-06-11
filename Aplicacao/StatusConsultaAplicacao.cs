using Comum.NotificationPattern;
using Dominio.Entidades;
using Dominio.DTOs;
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
    public class StatusConsultaAplicacao : IStatusConsultaAplicacao
    {
        private readonly IStatusConsultaRepositorio _statusRepositorio;
        private readonly StatusConsulta _statusDominio;

        public StatusConsultaAplicacao(IStatusConsultaRepositorio statusRepositorio)
        {
            _statusRepositorio = statusRepositorio;
            _statusDominio = new StatusConsulta();
        }

        public NotificationResult Salvar(StatusConsultaDTO statusConsultaDTO)
        {
            var notificationResult = new NotificationResult();
            try
            {
                var status = new StatusConsulta
                {
                    StatusConsultaId = statusConsultaDTO.StatusConsultaId,
                    Nome = statusConsultaDTO.Nome
                };

                _statusDominio.ValidarCampos(status, notificationResult);
                if (notificationResult.IsValid)
                {
                    if (status.StatusConsultaId == 0)
                    {
                        var statusExistente = _statusRepositorio.ObterPorNome(status.Nome);

                        if (statusExistente == null)
                        {
                            status = _statusRepositorio.Adicionar(status);

                            if(status.StatusConsultaId > 0)
                            {
                                notificationResult.Result = status;
                                notificationResult.Add("Status adicionado com sucesso!");
                                return notificationResult;                            }
                        }
                        else
                        {
                            notificationResult.Add("Status já existente!");
                            return notificationResult;
                        }

                    }
                    else
                    {
                        status = _statusRepositorio.Atualizar(status);
                        
                        if(status != null)
                        {
                            notificationResult.Result = status;
                            notificationResult.Add("Status atualizado com sucesso!");
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
                if (notificationResult.IsValid && id > 0)
                {
                    var statusExistente = _statusRepositorio.ObterPorId(id);

                    if (statusExistente != null)
                    {
                        notificationResult.Result = statusExistente;
                        notificationResult.Add("Status encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Status não encontrado!");
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
                    var listaStatus = _statusRepositorio.ObterTodos();

                    if (listaStatus.Count > 0)
                    {
                        notificationResult.Result = listaStatus;
                        notificationResult.Add("Lista criada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Nenhum status encontrado");
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
                    var resultadoExclusao = _statusRepositorio.ExcluirPorId(id);

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
