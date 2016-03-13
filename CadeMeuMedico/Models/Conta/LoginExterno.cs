using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models.Conta
{
    public class LoginExterno
    {
        public string Provedor { get; set; }
        public string ProvedorNomeDisplay { get; set; }
        public string ProvedorUsuarioId { get; set; }
    }
}