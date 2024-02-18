using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.sp_med_group.webApi.Domains;
using senai.sp_med_group.webApi.Interfaces;
using senai.sp_med_group.webApi.Repositories;
using senai.sp_med_group.webApi.ViewModel;
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
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Loga na aplicação
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel Login)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(Login.EmailUsuario, Login.SenhaUsuario);
                if (usuarioBuscado != null)
                {
                    var Claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim("role", usuarioBuscado.IdTipoUsuario.ToString())
                };

                    var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("spmedicalgroupwebapi"));

                    var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                            issuer: "senai.sp_med_group.webApi",
                            audience: "senai.sp_med_group.webApi",
                            claims: Claims,
                            expires: DateTime.Now.AddMinutes(50),
                            signingCredentials: Creds
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                    });
                }

                return NotFound("Email ou Senha Inválido!");
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }
    }
}
