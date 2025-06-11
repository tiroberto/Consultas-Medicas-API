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
    public class PacienteAplicacao : IPacienteAplicacao
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;
        private readonly Paciente _pacienteDominio;

        public PacienteAplicacao(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
            _pacienteDominio = new Paciente();
        }

        public NotificationResult Adicionar(PacienteDTO pacienteDTO)
        {
            var notificationResult = new NotificationResult();

            try
            {
                var paciente = new Paciente
                {
                    PacienteId = pacienteDTO.PacienteId,
                    Nome = pacienteDTO.Nome,
                    CPF = pacienteDTO.CPF,
                    DataNascimento = pacienteDTO.DataNascimento,
                    Email = pacienteDTO.Email,
                    Telefone = pacienteDTO.Telefone
                };
                _pacienteDominio.ValidarCampos(paciente, notificationResult);

                if (notificationResult.IsValid)
                {
                    var pacienteExistente = _pacienteRepositorio.VerificarExistenciaPaciente(paciente);
                    
                    if (!pacienteExistente)
                    {
                        paciente = _pacienteRepositorio.Adicionar(paciente);

                        if (paciente.PacienteId > 0)
                        {
                            notificationResult.Result = paciente; 
                            notificationResult.Add("Paciente cadastrado com sucesso!");
                            return notificationResult;                                
                        }
                    }
                    else
                    {
                        notificationResult.Add("Paciente já cadastrado!");
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

        public NotificationResult Atualizar(PacienteDTO pacienteDTO)
        {
            var notificationResult = new NotificationResult();

            try
            {
                var paciente = new Paciente
                {
                    PacienteId = pacienteDTO.PacienteId,
                    Nome = pacienteDTO.Nome,
                    CPF = pacienteDTO.CPF,
                    DataNascimento = pacienteDTO.DataNascimento,
                    Email = pacienteDTO.Email,
                    Telefone = pacienteDTO.Telefone
                };
                _pacienteDominio.ValidarCampos(paciente, notificationResult);

                if (notificationResult.IsValid)
                {
                    var pacienteExistente = _pacienteRepositorio.ObterPorId(paciente.PacienteId);

                    if (pacienteExistente != null)
                    {
                        pacienteExistente = _pacienteRepositorio.Atualizar(paciente);

                        if (pacienteExistente != null)
                        {
                            notificationResult.Result = pacienteExistente;
                            notificationResult.Add("Dados do paciente atualizados com sucesso!");
                            return notificationResult;
                        }
                    }
                    else
                    {
                        notificationResult.Add("Paciente não encontrado!");
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
                if(notificationResult.IsValid)
                {
                    var pacienteExistente = _pacienteRepositorio.ObterPorId(id);

                    if (pacienteExistente != null) 
                    {
                        notificationResult.Result= pacienteExistente;
                        notificationResult.Add("Paciente encontrado com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Paciente não encontrado!");
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

        public NotificationResult ObterPorCPF(string cpf)
        {
            var notificationResult = new NotificationResult();

            try
            {   
                if (notificationResult.IsValid) 
                {
                    var validacaoCPF = FuncoesComuns.ValidarCPF(cpf);

                    if (validacaoCPF)
                    {
                        var pacienteExistente = _pacienteRepositorio.ObterPorCPF(cpf);

                        if(pacienteExistente != null)
                        {
                            notificationResult.Result = pacienteExistente;
                            notificationResult.Add("Paciente encontrado com sucesso!");
                            return notificationResult;
                        }
                        else
                        {
                            notificationResult.Add("Paciente não encontrado!");
                        }
                    }
                    else
                    {
                        notificationResult.Add("CPF inválido!");
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
                        var pacienteExistente = _pacienteRepositorio.ObterPorEmail(email);

                        if (pacienteExistente != null)
                        {
                            notificationResult.Result = pacienteExistente;
                            notificationResult.Add("Paciente encontrado com sucesso!");
                            return notificationResult;
                        }
                        else
                        {
                            notificationResult.Add("Paciente não encontrado!");
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
                        var pacienteExistente = _pacienteRepositorio.ObterPorTelefone(telefone);

                        if (pacienteExistente != null)
                        {
                            notificationResult.Result = pacienteExistente;
                            notificationResult.Add("Paciente encontrado com sucesso!");
                            return notificationResult;
                        }
                        else
                        {
                            notificationResult.Add("Paciente não encontrado!");
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

        public NotificationResult ObterTodos()
        {
            var notificationResult = new NotificationResult();

            try
            {
                if (notificationResult.IsValid)
                {
                    var listaPacientes = _pacienteRepositorio.ObterTodos();

                    if (listaPacientes.Count > 0)
                    {
                        notificationResult.Result = listaPacientes;
                        notificationResult.Add("Lista de pacientes gerada com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Pacientes não encontrados!");
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
                    var resultadoExclusao = _pacienteRepositorio.ExcluirPorId(id);

                    if (resultadoExclusao)
                    {
                        notificationResult.Add("Paciente excluído com sucesso!");
                        return notificationResult;
                    }
                    else
                    {
                        notificationResult.Add("Paciente não encontrado!");
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
