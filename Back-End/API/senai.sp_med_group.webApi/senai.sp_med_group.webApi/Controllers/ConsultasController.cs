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
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private IConsultaRepository _consultaRepository { get; set; }

        public ConsultasController()
        {
            _consultaRepository = new ConsultaRepository();
        }

        /// <summary>
        /// Listar todas as consultas
        /// </summary>
        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                List<Consulta> listaConsulta = _consultaRepository.ListarTodas();
                if (listaConsulta == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não existem consultas agendadas"
                    });
                }
                return Ok(listaConsulta);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPost]
        public IActionResult Cadastrar(Consulta novaConsulta)
        {
            try
            {

                if (novaConsulta.IdMedico == null || novaConsulta.IdPaciente == null || novaConsulta.DataConsulta < DateTime.Now)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados estão incorretos"
                    });
                }
                _consultaRepository.CadastrarConsulta(novaConsulta);

                return StatusCode(201, new
                {
                    Mensagem = "Consulta cadastrada",
                    novaConsulta
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        /// <summary>
        /// Cancela uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPatch("Cancelar/{id:int}")]
        public IActionResult Cancelar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "O ID está incorreto"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não existe consulta com esse ID"
                    });
                }
                _consultaRepository.CancelarConsulta(id);

                return StatusCode(200, new
                {
                    Mensagem = "A consulta foi cancelada"
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        /// <summary>
        /// Realiza uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpPatch("Realizar/{id:int}")]
        public IActionResult Realizar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "O ID está incorreto"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não existe consulta com esse ID"
                    });
                }
                _consultaRepository.RealizarConsulta(id);

                return StatusCode(200, new
                {
                    Mensagem = "A consulta foi realizada"
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        /// <summary>
        /// Lista as consultas do usuário, sendo ele paciente ou médico
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "1, 2")]
        [HttpGet("Lista/Minhas")]
        public IActionResult ListarMinhas()
        {
            try
            {

                int id = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                int idTipoUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);

                List<Consulta> listaConsulta = _consultaRepository.ListarMinhasConsultas(id, idTipoUsuario);

                if (listaConsulta.Count == 0)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não existe consulta com esse usuário"
                    });
                }

                if (idTipoUsuario == 2)
                {
                    return Ok(new
                    {
                        Mensagem = $"O paciente buscado tem {_consultaRepository.ListarMinhasConsultas(id, idTipoUsuario).Count} consultas",
                        listaConsulta
                    });
                }
                if (idTipoUsuario == 1)
                {
                    return Ok(new
                    {
                        Mensagem = $"O médico buscado tem {_consultaRepository.ListarMinhasConsultas(id, idTipoUsuario).Count} consultas",
                        listaConsulta
                    });
                }
                return null;

            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        /// <summary>
        /// Altera a descrição de uma consulta
        /// </summary>
        /// <param name="consultaAtualizada"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpPatch("AlterarDescricao/{id}")]
        public IActionResult AlterarDescricao(Consulta consultaAtualizada, int id)
        {
            try
            {
                if (consultaAtualizada.Descricao == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe a descrição"
                    });
                }

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido"
                    });
                }

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não existe consulta com esse ID"
                    });
                }
                _consultaRepository.AlterarDescricao(consultaAtualizada.Descricao, id);
                return StatusCode(200, new
                {
                    Mensagem = "Descrição da consulta alterada",
                    consultaAtualizada
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Remove uma consulta da aplicação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpDelete("Remover/{id:int}")]
        public IActionResult RemoverConsulta(int id)
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

                if (_consultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não existe consulta com esse ID"
                    });
                }

                _consultaRepository.RemoverConsulta(id);

                return StatusCode(200, new
                {
                    Mensagem = "Consulta removida"
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
