using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("IDUsuario")]
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}