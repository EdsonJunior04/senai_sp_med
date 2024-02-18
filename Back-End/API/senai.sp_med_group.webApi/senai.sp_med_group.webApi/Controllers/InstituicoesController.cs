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
    public class InstituicoesController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository { get; set; }

        public InstituicoesController()
        {
            _instituicaoRepository = new InstituicaoRepository();
        }

        /// <summary>
        /// Cadastra uma nova clínica
        /// </summary>
        /// <param name="novaClinica"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPost]
        public IActionResult Cadastrar(Instituicao novaClinica)
        {
            try
            {

                if (novaClinica == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Algum valor é inválido"
                    });
                }
                _instituicaoRepository.Cadastrar(novaClinica);

                return StatusCode(201, new
                {
                    Mensagem = "Instituição cadastrada",
                    novaClinica
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        /// <summary>
        /// Lista todas as clínicas
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Instituicao> listaInstuicao = _instituicaoRepository.ListarTodas();

                if (listaInstuicao == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não existe instituição cadastrada"
                    });
                }

                return Ok(new
                {
                    Mensagem = $"Foram encontradas {listaInstuicao.Count()} instituições",
                    listaInstuicao
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma clínica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clinicaAtualizada"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Instituicao clinicaAtualizada)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Id inválido"
                    });
                }

                if (_instituicaoRepository.BuscarPorId(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não existe clínica com esse ID"
                    });
                }
                if (clinicaAtualizada.Cnpj == null || clinicaAtualizada.Endereco == null || clinicaAtualizada.NomeFantasia == null || clinicaAtualizada.RazaoSocial == null || clinicaAtualizada.Cnpj.Length != 14)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Dados inválidos"
                    });
                }

                _instituicaoRepository.Atualizar(id, clinicaAtualizada);
                return Ok(new
                {
                    Mensagem = "A clínica foi atualizada com sucesso!",
                    clinicaAtualizada
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Deleta uma clínica da aplicação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpDelete("{id:int}")]
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

                if (_instituicaoRepository.BuscarPorId(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não existe clínica com esse ID"
                    });
                }

                _instituicaoRepository.Deletar(id);
                return Ok(new
                {
                    Mensagem = "Clínica removida",
                });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }


        }
    }
}
