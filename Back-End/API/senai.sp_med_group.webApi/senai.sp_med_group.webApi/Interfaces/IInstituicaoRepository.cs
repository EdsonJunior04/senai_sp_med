using senai.sp_med_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Interfaces
{
    interface IInstituicaoRepository
    {
        void Atualizar(int id, Instituicao clinicaAtualizada);
        void Deletar(int id);
        void Cadastrar(Instituicao novaClinica);
        Instituicao BuscarPorId(int id);
        List<Instituicao> ListarTodas();
    }
}
