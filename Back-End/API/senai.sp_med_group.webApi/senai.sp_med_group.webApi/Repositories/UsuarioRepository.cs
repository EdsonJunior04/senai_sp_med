using Microsoft.AspNetCore.Http;
using senai.sp_med_group.webApi.Context;
using senai.sp_med_group.webApi.Domains;
using senai.sp_med_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SpMedContext ctx = new SpMedContext();

        public void Atualizar(int id, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = BuscarPorId(id);

            if (usuarioAtualizado.Nome != null || usuarioAtualizado.Senha != null || usuarioAtualizado.Email != null)
            {
                usuarioBuscado.Nome = usuarioAtualizado.Nome;
                usuarioBuscado.Email = usuarioAtualizado.Email;
                usuarioBuscado.Senha = usuarioAtualizado.Senha;

                ctx.Usuarios.Update(usuarioBuscado);

                ctx.SaveChanges();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);

            ctx.SaveChanges();
        }

        public string ConsultarPerfilBD(int id_usuario)
        {
            Imagemusuario imagemUsuario = new Imagemusuario();

            imagemUsuario = ctx.Imagemusuarios.FirstOrDefault(i => i.IdUsuario == id_usuario);

            if (imagemUsuario != null)
            {                
                return Convert.ToBase64String(imagemUsuario.Binario);
            }

            return null;
        }

        public string ConsultarPerfilDir(int id_usuario)
        {
            string nome_novo = id_usuario.ToString() + ".png";
            string caminho = Path.Combine("Perfil", nome_novo);

            
            if (File.Exists(caminho))
            {
                
                byte[] bytesArquivo = File.ReadAllBytes(caminho);
                
                return Convert.ToBase64String(bytesArquivo);
            }

            return null;

        }

        /// <summary>
        /// Deleta um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário que será deletado</param>
        public void Deletar(int id)
        {            
            ctx.Usuarios.Remove(BuscarPorId(id));            
            ctx.SaveChanges();
        }


        public List<Usuario> ListarUsuarios()
        {
            return ctx.Usuarios
                .Select(u => new Usuario
                {
                    Email = u.Email,
                    Nome = u.Nome,
                    IdTipoUsuarioNavigation = new Tipousuario()
                    {
                        Tipo = u.IdTipoUsuarioNavigation.Tipo
                    }
                }).ToList();
        }

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public void SalvarPerfilBD(IFormFile foto, int id_usuario)
        {
            Imagemusuario imagemUsuario = new Imagemusuario();

            using (var ms = new MemoryStream())
            {
                
                foto.CopyTo(ms);
                
                imagemUsuario.Binario = ms.ToArray();
                
                imagemUsuario.NomeArquivo = foto.FileName;
                
                imagemUsuario.MimeType = foto.FileName.Split('.').Last();
                
                imagemUsuario.IdUsuario = id_usuario;
            }

            
            Imagemusuario fotoexistente = new Imagemusuario();
            fotoexistente = ctx.Imagemusuarios.FirstOrDefault(i => i.IdUsuario == id_usuario);

            if (fotoexistente != null)
            {
                fotoexistente.Binario = imagemUsuario.Binario;
                fotoexistente.NomeArquivo = imagemUsuario.NomeArquivo;
                fotoexistente.MimeType = imagemUsuario.MimeType;
                fotoexistente.IdUsuario = id_usuario;

                
                ctx.Imagemusuarios.Update(fotoexistente);
            }
            else
            {
                ctx.Imagemusuarios.Add(imagemUsuario);
            }

            
            ctx.SaveChanges();
        }

        public void SalvarPerfilDir(IFormFile foto, int id_usuario)
        {

            
            string nome_novo = id_usuario.ToString() + ".png";



            using (var stream = new FileStream(Path.Combine("perfil", nome_novo), FileMode.Create)) 
            {
                
                foto.CopyTo(stream);
            }
        }
    }
}
