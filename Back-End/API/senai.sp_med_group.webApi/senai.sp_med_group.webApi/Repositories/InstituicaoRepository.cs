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
    public class InstituicaoRepository : IInstituicaoRepository
    {
        SpMedContext ctx = new SpMedContext();

        public void Atualizar(int id, Instituicao clinicaAtualizada)
        {
            Instituicao clinicaBuscada = BuscarPorId(id);
            if (clinicaAtualizada.NomeFantasia != null || clinicaAtualizada.RazaoSocial != null || clinicaAtualizada.Cnpj != null || clinicaAtualizada.Endereco != null)
            {
                clinicaBuscada.NomeFantasia = clinicaAtualizada.NomeFantasia;
                clinicaBuscada.RazaoSocial = clinicaAtualizada.RazaoSocial;
                clinicaBuscada.Cnpj = clinicaAtualizada.Cnpj;
                clinicaBuscada.Endereco = clinicaAtualizada.Endereco;

                ctx.Instituicaos.Update(clinicaBuscada);

                ctx.SaveChanges();
            }
        }

        public Instituicao BuscarPorId(int id)
        {
            return ctx.Instituicaos.FirstOrDefault(i => i.IdInstituicao == id);
        }

        public void Cadastrar(Instituicao novaClinica)
        {
            ctx.Instituicaos.Add(novaClinica);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Instituicaos.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<Instituicao> ListarTodas()
        {
            return ctx.Instituicaos.Include(c => c.Medicos).ToList();
        }
    }
}
