using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_med_group.webApi.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdMedico { get; set; }
        public short? IdEspecializacao { get; set; }
        public short? IdInstituicao { get; set; }
        public int? IdUsuario { get; set; }
        public string Crm { get; set; }

        public virtual Especializacao IdEspecializacaoNavigation { get; set; }
        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
