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
    public class PacienteRepository : IPacienteRepository
    {
        SpMedContext ctx = new SpMedContext();

        public void Atualizar(int id, Paciente pacienteAtualizado)
        {
            Paciente pacienteBuscado = BuscarPorId(id);

            if (pacienteAtualizado.Endereco != null || pacienteAtualizado.Telefone != null || pacienteAtualizado.Rg != null || pacienteAtualizado.Cpf != null || pacienteAtualizado.DataNascimento < DateTime.Now)
            {
                pacienteBuscado.Endereco = pacienteAtualizado.Endereco;
                pacienteBuscado.Telefone = pacienteAtualizado.Telefone;
                pacienteBuscado.Rg = pacienteAtualizado.Rg;
                pacienteBuscado.Cpf = pacienteAtualizado.Cpf;
                pacienteBuscado.IdUsuario = pacienteAtualizado.IdUsuario;
                pacienteBuscado.DataNascimento = pacienteAtualizado.DataNascimento;

                ctx.Pacientes.Update(pacienteBuscado);

                ctx.SaveChanges();
            }
        }

        public Paciente BuscarPorId(int id)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
        }

        public void Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Pacientes.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes.Include(p => p.IdUsuarioNavigation).ToList();
        }
    }
}
