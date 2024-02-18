using senai.sp_med_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> ListarTodos();
        void Cadastrar(Medico novoMedico);
    }
}
