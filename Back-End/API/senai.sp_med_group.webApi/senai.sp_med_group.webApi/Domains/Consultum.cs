using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.sp_med_group.webApi.Domains
{
    public partial class Consulta
    {
        [Key]
        public int IdConsulta { get; set; }

        [Required(ErrorMessage = "O médico da consulta é obrigatório!")]
        public int? IdMedico { get; set; }
        public byte? IdSituacao { get; set; }
        public int? IdPaciente { get; set; }

        [Required(ErrorMessage = "A Data da consulta é obrigatória!")]
        public DateTime DataConsulta { get; set; }
        public string Descricao { get; set; }

        public virtual Medico IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Situacao IdSituacaoNavigation { get; set; }
    }
}
