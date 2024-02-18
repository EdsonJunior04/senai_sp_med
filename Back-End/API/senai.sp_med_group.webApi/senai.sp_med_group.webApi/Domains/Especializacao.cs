using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_med_group.webApi.Domains
{
    public partial class Especializacao
    {
        public Especializacao()
        {
            Medicos = new HashSet<Medico>();
        }

        public short IdEspecializacao { get; set; }
        public string TituloEspecializacao { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
