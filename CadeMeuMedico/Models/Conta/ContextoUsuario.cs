using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class ContextoUsuario : DbContext
    {
        public ContextoUsuario()
            :base("ConexaoCadeMeuMedico")
        {}

        public DbSet<Usuario> Usuarios { get; set; }
    }
}