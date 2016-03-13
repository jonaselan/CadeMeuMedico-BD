using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class Login
    {
        [Required]
        [Display(Name = "Nome do Usuário")]
        public string UsuarioNome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Relembrar?")]
        public bool Relembrar { get; set; }
    }
}