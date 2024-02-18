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
    public class MedicosController : ControllerBase
    {
        private IMedicoRepository _medicoRepository { get; set; }

        public MedicosController()
        {
            _medicoRepository = new MedicoRepository();
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpGet]
        public IActionResult Listar()
        {
            List<Medico> Lista = _medicoRepository.ListarTodos();

            return Ok(Lista);
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoMedico"></param>
        /// <returns></returns>
        [Authorize(Roles = "3")]
        [HttpPost]
        public IActionResult Cadastrar(Medico novoMedico)
        {
            try
            {
                if (novoMedico.Crm == null || novoMedico.IdUsuario == null || novoMedico.IdEspecializacao <= 0 || novoMedico.IdInstituicao <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Dados inválidos"
                    });
                }

                _medicoRepository.Cadastrar(novoMedico);

                return Ok(new
                {
                    Mensagem = "Médico cadastrado",
                    novoMedico
                });
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }
    }
}
