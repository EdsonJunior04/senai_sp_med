using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.sp_med_group.webApi.Domains;
using senai.sp_med_group.webApi.Interfaces;
using senai.sp_med_group.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }      

        [Authorize(Roles = "3")]
        [HttpGet]
        public IActionResult Listar()
        {
            if (_usuarioRepository.ListarUsuarios() == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não existe algum usuario cadastrado"
                });
            }

            return Ok(_usuarioRepository.ListarUsuarios());
        }

        //[Authorize(Roles = "3")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUsuario)
        {
            try
            {
                if (novoUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Todos dados são obrigatórios"
                    });
                }
                _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201, new
                {
                    Mensagem = "Usuario cadastrado",
                    novoUsuario
                });
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }

        }

        //[Authorize(Roles = "3")]
        [HttpPut("att/{id}")]
        public IActionResult Atualizar(int id, Usuario usuarioAtualizado)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não existe usuário com esse ID"
                    });
                }
                if (usuarioAtualizado == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Todos dados são obrigatórios"
                    });
                }
                _usuarioRepository.Atualizar(id, usuarioAtualizado);
                return StatusCode(200, new
                {
                    Mensagem = "Usuario atualizado",
                    usuarioAtualizado
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        [Authorize(Roles = "3")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não existe usuário com esse ID"
                    });
                }

                _usuarioRepository.Deletar(id);

                return StatusCode(200, new
                {
                    Mensagem = "Usuario deletado"
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }


    }
}
