using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.sp_med_group.webApi.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatório!")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        public string SenhaUsuario { get; set; }
    }
}
