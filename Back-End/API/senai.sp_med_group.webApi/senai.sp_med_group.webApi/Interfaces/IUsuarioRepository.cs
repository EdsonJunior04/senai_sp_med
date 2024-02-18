using Microsoft.AspNetCore.Http;
using senai.sp_med_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);
        List<Usuario> ListarUsuarios();
        void Cadastrar(Usuario novoUser);
        void Deletar(int id);
        Usuario BuscarPorId(int id);
        void Atualizar(int id, Usuario userAtt);
        void SalvarPerfilBD(IFormFile foto, int id_usuario);
        string ConsultarPerfilBD(int id_usuario);
        void SalvarPerfilDir(IFormFile foto, int id_usuario);
        string ConsultarPerfilDir(int id_usuario);
    }
}
