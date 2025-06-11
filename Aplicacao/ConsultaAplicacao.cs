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
    public class ConsultaAplicacao : IConsultaAplicacao
    {
        private readonly IConsultaRepositorio _consultaRepositorio;
        private readonly Consulta _consultaDominio;

        public ConsultaAplicacao(IConsultaRepositorio consultaRepositorio)
        {
            _consultaRepositorio = consultaRepositorio;
            _consultaDominio = new Consulta();
        }

        public NotificationResult Adicionar(ConsultaDTO consultaDTO)
        {
            var notificationResult = new NotificationResult();

            try
            {
                var consulta = new Consulta
                {
                    ConsultaId = consultaDTO.ConsultaId,
                    Paciente = new Paciente { PacienteId = consultaDTO.PacienteId },
                    Medico = new Medico { MedicoId = consultaDTO.MedicoId },
                    DataHoraInicio = consultaDTO.DataHoraInicio,
                    DataHoraFim = consultaDTO.DataHoraFim,
                    Observacoes = consultaDTO.Observacoes,
                    StatusConsulta = new StatusConsulta { StatusConsultaId = consultaDTO.StatusConsultaId }
                };
                _consultaDominio.ValidarCampos(consulta, notificationResult);

                if (notificationResult.IsValid)
                {
                    var consultaExistente = _consultaRepositorio.ObterPorId(consulta.ConsultaId);

                    if (consultaExistente == null)
                    {
                        consulta.StatusConsulta.StatusConsultaId = 1; //padrão 1=agendada                    
                        consulta = _consultaRepositorio.Adicionar(consulta);

                        if (consulta.ConsultaId > 0)
                        {
                            notificationResult.Result = consulta;
                            notificationResult.Add("Consulta adicionada com sucesso!");
                            return notificationResult;
                        }                        
                    }
                    else
                    {
                        notificationResult.Result = consulta;
                        notificationResult.Add("Consulta já existe!");
                    }
                }

                return notificationResult;
            }
            catch (Exception e)
            {
                return notificationResult.Add(new NotificationError(e.Message));
            }
        }

        public NotificationResult Atualizar(ConsultaDTO consultaDTO)
        {
            var notificationResult = new NotificationResult();

            try
            {
                var consulta = new Consulta
                {
                    ConsultaId = consultaDTO.ConsultaId,
                    Paciente = new Paciente { PacienteId = consultaDTO.PacienteId },
                    Medico = new Medico { MedicoId = consultaDTO.MedicoId },
                    DataHoraInicio = consultaDTO.DataHoraInicio,
                    DataHoraFim = consultaDTO.DataHoraFim,
                    Observacoes = consultaDTO.Observacoes,
                    StatusConsulta = new StatusConsulta { StatusConsultaId = consultaDTO.StatusConsultaId }
                };

                _consultaDominio.ValidarCampos(consulta, notificationResult);

                if (notificationResult.IsValid)
                {
                    var consultaExistente = _consultaRepositorio.ObterPorId(consulta.ConsultaId);

                    if (consultaExistente != null)
                    {
                        consulta = _consultaRepositorio.Atualizar(consulta);

                        if (consulta != null)
                        {
                            notificationResult.Result = consulta;
                            notificationResult.Add("Dados da consulta atualizados com sucesso!");
                            return notificationResult;
                        }
                    }
                    else
                    {
                        notificationResult.Add("Consulta inexistente!");
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
                    var consultaExistente = _consultaRepositorio.ObterPorId(id);

                    if (consultaExistente != null)
                    {
                        notificationResult.Result = consultaExistente;
                        notificationResult.Add("Consulta encontrada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consulta não encontrada!");
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
                    var listaConsultas = _consultaRepositorio.ObterTodos();

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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
                    var resultadoExclusao = _consultaRepositorio.ExcluirPorId(id);

                    if (resultadoExclusao)
                    {
                        notificationResult.Add("Consulta excluída com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consulta não encontrada!");
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

        public NotificationResult ObterPorStatusConsulta(int statusId)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaConsultas = _consultaRepositorio.ObterPorStatusConsulta(statusId);

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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

        public NotificationResult ObterPorPaciente(int pacienteId)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaConsultas = _consultaRepositorio.ObterPorPaciente(pacienteId);

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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

        public NotificationResult ObterPorMedico(int medicoId)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaConsultas = _consultaRepositorio.ObterPorMedico(medicoId);

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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

        public NotificationResult ObterPorData(DateTime data)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaConsultas = _consultaRepositorio.ObterPorData(data);

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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

        public NotificationResult ObterPorIntervaloDataHora(DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaConsultas = _consultaRepositorio.ObterPorIntervaloDataHora(dataHoraInicio, dataHoraFim);

                    if (listaConsultas.Count > 0)
                    {
                        listaConsultas.ForEach(c => c.Medico = new Medico
                        {
                            MedicoId = c.Medico.MedicoId,
                            Nome = c.Medico.Nome,
                            CRM = c.Medico.CRM,
                            Email = c.Medico.Email,
                            Telefone = c.Medico.Telefone,
                            Especialidade = new Especialidade { EspecialidadeId = c.Medico.Especialidade.EspecialidadeId, Nome = c.Medico.Especialidade.Nome }
                        });
                        listaConsultas.ForEach(c => c.Paciente = new Paciente
                        {
                            PacienteId = c.Paciente.PacienteId,
                            Nome = c.Paciente.Nome,
                            CPF = c.Paciente.CPF,
                            Email = c.Paciente.Email,
                            Telefone = c.Paciente.Telefone,
                            DataNascimento = c.Paciente.DataNascimento
                        });
                        notificationResult.Result = listaConsultas;
                        notificationResult.Add("Lista de consultas gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Consultas não encontradas!");
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
