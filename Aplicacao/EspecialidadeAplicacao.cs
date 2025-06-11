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
    public class EspecialidadeAplicacao : IEspecialidadeAplicacao
    {
        private readonly IEspecialidadeRepositorio _especialidadeRepositorio;
        private readonly Especialidade _especialidadeDominio;

        public EspecialidadeAplicacao(IEspecialidadeRepositorio especialidadeRepositorio)
        {
            _especialidadeRepositorio = especialidadeRepositorio;
            _especialidadeDominio = new Especialidade();
        }

        public NotificationResult Adicionar(EspecialidadeDTO especialidadeDTO)
        {
            var notificationResult = new NotificationResult();
            try
            {
                var especialidade = new Especialidade
                {
                    EspecialidadeId = especialidadeDTO.EspecialidadeId,
                    Nome = especialidadeDTO.Nome
                };
                _especialidadeDominio.ValidarCampos(especialidade, notificationResult);

                if (notificationResult.IsValid)
                {
                    var especialidadeExistente = _especialidadeRepositorio.ObterPorNome(especialidade.Nome);

                    if (especialidadeExistente == null)
                    {
                        especialidade = _especialidadeRepositorio.Adicionar(especialidade);

                        if(especialidade.EspecialidadeId > 0)
                        {
                            notificationResult.Result = especialidade;
                            notificationResult.Add("Especialidade adicionada com sucesso!");
                            return notificationResult;
                        }
                     
                    else
                    {
                        notificationResult.Add("Especialidade já existente!");
                        return notificationResult;
                    }

                    }
                    else
                    {
                        especialidade = _especialidadeRepositorio.Atualizar(especialidade);

                        if(especialidade != null)
                        {
                            notificationResult.Result = especialidade;
                            notificationResult.Add("Especialidade atualizado com sucesso!");
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

        public NotificationResult Atualizar(EspecialidadeDTO especialidadeDTO)
        {
            var notificationResult = new NotificationResult();
            try
            {
                var especialidade = new Especialidade
                {
                    EspecialidadeId = especialidadeDTO.EspecialidadeId,
                    Nome = especialidadeDTO.Nome
                };
                _especialidadeDominio.ValidarCampos(especialidade, notificationResult);

                if (notificationResult.IsValid)
                {
                    var especialidadeExistente = _especialidadeRepositorio.ObterPorNome(especialidade.Nome);

                    if (especialidadeExistente != null)
                    {
                        especialidadeExistente = _especialidadeRepositorio.Atualizar(especialidadeExistente);

                        if (especialidadeExistente != null)
                        {
                            notificationResult.Result = especialidadeExistente;
                            notificationResult.Add("Especialidade atualizada com sucesso!");
                            return notificationResult;
                        }
                    }
                    else
                    {
                        notificationResult.Add("Especialidade não possui cadastro!");
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
                    var especialidadeExistente = _especialidadeRepositorio.ObterPorId(id);

                    if (especialidadeExistente != null)
                    {
                        notificationResult.Result = especialidadeExistente;
                        notificationResult.Add("Especialidade encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Especialidade não encontrado!");
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
                    var listaEspecialidades = _especialidadeRepositorio.ObterTodos();

                    if (listaEspecialidades.Count > 0)
                    {
                        notificationResult.Result = listaEspecialidades;
                        notificationResult.Add("Lista criada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Nenhum especialidade encontrado");
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
                    var resultadoExclusao = _especialidadeRepositorio.ExcluirPorId(id);

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
