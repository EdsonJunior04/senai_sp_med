using senai.sp_med_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Interfaces
{
    interface IConsultaRepository
    {
        void CadastrarConsulta(Consulta novaConsulta);
        void AlterarDescricao(string descricao, int id);
        void RemoverConsulta(int id);
        void CancelarConsulta(int Id);
        void RealizarConsulta(int Id);
        Consulta BuscarPorId(int id);
        List<Consulta> ListarMinhasConsultas(int id, int idTipoUsuario);
        List<Consulta> ListarTodas();
    }
}
