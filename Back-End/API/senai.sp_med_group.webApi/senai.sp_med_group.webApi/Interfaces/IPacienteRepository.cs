using senai.sp_med_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Interfaces
{
    interface IPacienteRepository
    {
        void Atualizar(int id, Paciente pacienteAtualizado);
        void Deletar(int id);
        void Cadastrar(Paciente novoPaciente);
        Paciente BuscarPorId(int id);
        List<Paciente> ListarTodos();
    }
}
