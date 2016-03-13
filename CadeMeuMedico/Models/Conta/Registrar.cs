using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class Registrar
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Nome do Usuário")]
        public string UsuarioNome { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem que ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "A senha nova e a senha de confirmação são diferentes.")]
        public string ConfirmarSenha { get; set; }
    }
}