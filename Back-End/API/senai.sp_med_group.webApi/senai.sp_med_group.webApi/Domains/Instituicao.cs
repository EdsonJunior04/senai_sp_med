using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_med_group.webApi.Domains
{
    public partial class Instituicao
    {
        public Instituicao()
        {
            Medicos = new HashSet<Medico>();
        }

        public short IdInstituicao { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string Cnpj { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
