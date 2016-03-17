using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models
{
    public class MedicoProfile
    {
        public Medico Medico { get; set; }
        public Cidade Cidade { get; set; }
        public Especialidade Especialidade { get; set; }
    }
}