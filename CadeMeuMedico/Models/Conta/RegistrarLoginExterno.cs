using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class RegistrarLoginExterno
    {
        [Required]
        [Display(Name = "Nome do Usuário")]
        public string UsuarioNome { get; set; }

        public string DadosLoginExterno { get; set; }
    }
}