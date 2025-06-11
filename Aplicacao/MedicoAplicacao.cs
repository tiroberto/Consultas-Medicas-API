using Comum;
using Comum.NotificationPattern;
using Dominio.DTOs;
using Dominio.Entidades;
using Dominio.Interfaces.Aplicacao;
using Dominio.Interfaces.Repositorio;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class MedicoAplicacao : IMedicoAplicacao
    {
        private readonly IMedicoRepositorio _medicoRepositorio;
        private readonly Medico _medicoDominio;

        public MedicoAplicacao(IMedicoRepositorio medicoRepositorio)
        {
            _medicoRepositorio = medicoRepositorio;
            _medicoDominio = new Medico();
        }

        public NotificationResult Salvar(MedicoDTO medicoDTO)
        {
            var notificationResult = new NotificationResult();

            try
            {
                var medico = new Medico
                {
                    MedicoId = medicoDTO.MedicoId,
                    Nome = medicoDTO.Nome,
                    CRM = medicoDTO.CRM,
                    Email = medicoDTO.Email,
                    Telefone = medicoDTO.Telefone,
                    Especialidade = new Especialidade { EspecialidadeId = medicoDTO.EspecialidadeId}
                };

                _medicoDominio.ValidarCampos(medico, notificationResult);
                if (notificationResult.IsValid)
                {
                    if (medico.MedicoId == 0)
                    {
                        var medicoExistente = _medicoRepositorio.VerificarExistenciaMedico(medico);

                        if (!medicoExistente)
                        {
                            medico = _medicoRepositorio.Adicionar(medico);

                            if (medico.MedicoId > 0)
                            {
                                notificationResult.Result = medico;
                                notificationResult.Add("Médico cadastrado com sucesso!");
                                return notificationResult;
                            }
                        }
                        else
                        {
                            notificationResult.Add("Médico já cadastrado!");
                            return notificationResult;                            
                        }
                    }
                    else
                    {
                        medico = _medicoRepositorio.Atualizar(medico);

                        if(medico != null)
                        {
                            notificationResult.Result = medico;
                            notificationResult.Add("Dados do médico atualizados com sucesso!");
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
                if (notificationResult.IsValid)
                {
                    var medicoExistente = _medicoRepositorio.ObterPorId(id);

                    if (medicoExistente != null)
                    {
                        notificationResult.Result = medicoExistente;
                        notificationResult.Add("Medico encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Medico não encontrado!");
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

        public NotificationResult ObterPorCRM(string crm)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var medicoExistente = _medicoRepositorio.ObterPorCRM(crm);

                    if (medicoExistente != null)
                    {
                        notificationResult.Result = medicoExistente;
                        notificationResult.Add("Medico encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Medico não encontrado!");
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

        public NotificationResult ObterPorEmail(string email)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var validacaoEmail = FuncoesComuns.ValidarEmail(email);

                    if (validacaoEmail)
                    {
                        var medicoExistente = _medicoRepositorio.ObterPorEmail(email);

                        if (medicoExistente != null)
                        {
                            notificationResult.Result = medicoExistente;
                            notificationResult.Add("Medico encontrado com sucesso!");
                            return notificationResult;
                        }
                        else
                        {
                            notificationResult.Add("Medico não encontrado!");
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

        public NotificationResult ObterPorTelefone(string telefone)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var validacaoTelefone = FuncoesComuns.ValidarTelefone(telefone);

                    if (validacaoTelefone)
                    {
                        var medicoExistente = _medicoRepositorio.ObterPorTelefone(telefone);

                        if (medicoExistente != null)
                        {
                            notificationResult.Result = medicoExistente;
                            notificationResult.Add("Medico encontrado com sucesso!");
                            return notificationResult;
                        }
                        else
                        {
                            notificationResult.Add("Medico não encontrado!");
                        }
                    }
                    else
                    {
                        notificationResult.Add("Número de telefone inválido!");
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

        public NotificationResult ObterPorEspecialidade(int especialidadeId)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaMedicos = _medicoRepositorio.ObterPorEspecialidade(especialidadeId);

                    if(listaMedicos.Count > 0)
                    {
                        listaMedicos.ForEach(m => m.Especialidade = new Especialidade 
                        { 
                            EspecialidadeId = m.Especialidade.EspecialidadeId,
                            Nome = m.Especialidade.Nome,
                            Medicos = null  
                        });
                        notificationResult.Result = listaMedicos;
                        notificationResult.Add("Lista de médicos gerada com sucesso");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Médicos não encontrados");
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
                    var listaMedicos = _medicoRepositorio.ObterTodos();

                    if (listaMedicos.Count > 0)
                    {
                        listaMedicos.ForEach(m => m.Especialidade = new Especialidade
                        {
                            EspecialidadeId = m.Especialidade.EspecialidadeId,
                            Nome = m.Especialidade.Nome,
                            Medicos = null                          
                        });
                        notificationResult.Result = listaMedicos;
                        notificationResult.Add("Lista de medicos gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Medicos não encontrados!");
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
                    var resultadoExclusao = _medicoRepositorio.ExcluirPorId(id);

                    if (resultadoExclusao)
                    {
                        notificationResult.Add("Medico excluído com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Medico não encontrado!");
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
