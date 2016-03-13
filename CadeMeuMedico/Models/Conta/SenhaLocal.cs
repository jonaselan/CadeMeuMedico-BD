using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class SenhaLocal
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string SenhaAntiga { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem que ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha nova")]
        public string SenhaNova { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar a senha nova")]
        [Compare("SenhaNova", ErrorMessage = "A senha nova e a senha de confirmação são diferentes.")]
        public string ConfirmarSenha { get; set; }
    }
}