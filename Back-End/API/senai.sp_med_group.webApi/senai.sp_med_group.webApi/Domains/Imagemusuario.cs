using System;
using System.Collections.Generic;

#nullable disable

namespace senai.sp_med_group.webApi.Domains
{
    public partial class Imagemusuario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public byte[] Binario { get; set; }
        public string MimeType { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime? DataInclusao { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
