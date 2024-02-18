using Microsoft.EntityFrameworkCore;
using senai.sp_med_group.webApi.Context;
using senai.sp_med_group.webApi.Domains;
using senai.sp_med_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        SpMedContext ctx = new SpMedContext();

        public void AlterarDescricao(string descricao, int id)
        {
            Consulta consultaBuscado = BuscarPorId(id);
            if (descricao != null)
            {
                consultaBuscado.Descricao = descricao;
                ctx.Consulta.Update(consultaBuscado);
                ctx.SaveChanges();
            };
        }

        public Consulta BuscarPorId(int id)
        {
            return ctx.Consulta.FirstOrDefault(c => c.IdConsulta == id);
        }

        public void CadastrarConsulta(Consulta novaConsulta)
        {
            novaConsulta.Descricao = "Sem descrição definida";
            novaConsulta.IdSituacao = 1;
            ctx.Consulta.Add(novaConsulta);
            ctx.SaveChanges();
        }

        public void CancelarConsulta(int Id)
        {
            Consulta consultaBuscada = BuscarPorId(Id);
            consultaBuscada.IdSituacao = 2;
            consultaBuscada.Descricao = "Consulta Cancelada";
            ctx.Consulta.Update(consultaBuscada);
            ctx.SaveChanges();
        }
        public void RealizarConsulta(int Id)
        {
            Consulta consultaBuscada = BuscarPorId(Id);
            consultaBuscada.IdSituacao = 3;
            consultaBuscada.Descricao = "Consulta Realizada";
            ctx.Consulta.Update(consultaBuscada);
            ctx.SaveChanges();
        }

        public List<Consulta> ListarMinhasConsultas(int id, int idTipoUsuario)
        {
            if (idTipoUsuario == 1)
            {
                Medico medico = ctx.Medicos.FirstOrDefault(u => u.IdUsuario == id);

                int idMedico = medico.IdMedico;

                return ctx.Consulta
                                .Where(c => c.IdMedico == idMedico)
                                .Select(p => new Consulta()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    Descricao = p.Descricao,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Descricao = p.IdSituacaoNavigation.Descricao
                                    }


                                })
                                .ToList();
            }
            else if (idTipoUsuario == 2)
            {
                Paciente paciente = ctx.Pacientes.FirstOrDefault(u => u.IdUsuario == id);

                int idPaciente = paciente.IdPaciente;

                return ctx.Consulta
                                .Where(c => c.IdPaciente == idPaciente)
                                .Select(p => new Consulta()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    Descricao = p.Descricao,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Descricao = p.IdSituacaoNavigation.Descricao
                                    }


                                })
                                .ToList();
            }

            return null;

        }

        public List<Consulta> ListarTodas()
        {
            return ctx.Consulta
            .Select(p => new Consulta()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    Descricao = p.Descricao,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdMedicoNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Nome = p.IdPacienteNavigation.IdUsuarioNavigation.Nome,
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Descricao = p.IdSituacaoNavigation.Descricao
                                    }


                                })
                // .Include(c => c.IdMedicoNavigation)
                // .ThenInclude(c => c.IdUsuarioNavigation)
                // .Include(c => c.IdPacienteNavigation)
                // .Include(c => c.IdSituacaoNavigation)
                .ToList();
        }

        public void RemoverConsulta(int id)
        {
            ctx.Consulta.Remove(BuscarPorId(id));
            ctx.SaveChanges();
        }
    }
}
