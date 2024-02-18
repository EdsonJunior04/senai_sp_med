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
    public class MedicoRepository : IMedicoRepository
    {
        SpMedContext ctx = new SpMedContext();
        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.Include(c => c.IdUsuarioNavigation)
                .ToList();
        }
        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }
    }
}
