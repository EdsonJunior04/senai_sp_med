using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.sp_med_group.webApi.Domains;
using senai.sp_med_group.webApi.Interfaces;
using senai.sp_med_group.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository { get; set; }

        public PacientesController()
        {
            _pacienteRepository = new PacienteRepository();
        }

        /// <summary>
        /// Lista todos pacientes
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Paciente> lista = _pacienteRepository.ListarTodos();

                if (lista == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não existe paciente cadastrado"
                    });
                }

                return Ok(new
                {
                    Mensagem = $"Foram encontrados {lista.Count} pacientes",
                    lista
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPost]
        public IActionResult Cadastrar(Paciente novoPaciente)
        {
            try
            {
                if (novoPaciente.Cpf == null || novoPaciente.DataNascimento > DateTime.Now || novoPaciente.Rg == null || novoPaciente.Telefone == null || novoPaciente.Endereco == null || novoPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Dados inválidos"
                    });
                }

                _pacienteRepository.Cadastrar(novoPaciente);

                return Ok(new
                {
                    Mensagem = "Paciente cadastrado",
                    novoPaciente
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um paciente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pacienteAtualizado"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Paciente pacienteAtualizado)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Insira um ID válido!"
                    });
                }

                if (_pacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há nenhum paciente com o ID informado!"
                    });
                }

                if (pacienteAtualizado.Cpf == null || pacienteAtualizado.DataNascimento > DateTime.Now || pacienteAtualizado.Rg == null || pacienteAtualizado.Telefone == null || pacienteAtualizado.Endereco == null || pacienteAtualizado.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados informados são inválidos!"
                    });
                }

                _pacienteRepository.Atualizar(id, pacienteAtualizado);
                return Ok(new
                {
                    Mensagem = "O Paciente foi atualizado com sucesso!",
                    pacienteAtualizado
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Deleta um paciente da aplicação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "ID inválido"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não existe paciente com esse ID"
                });
            }

            _pacienteRepository.Deletar(id);
            return Ok(new
            {
                Mensagem = "Paciente removido",

            });
        }

        /// <summary>
        /// Busca um paciente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpGet("{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "ID inválido"
                });
            }

            if (_pacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não existe paciente com esse ID"
                });
            }

            Paciente pacienteEncontrado = _pacienteRepository.BuscarPorId(id);
            return Ok(new
            {
                Mensagem = "Paciente encontrado",
                pacienteEncontrado
            });
        }
    }
}
