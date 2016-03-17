using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models
{
    public class SelectEspecialidadeModel
    {
        public long IDEspecialidade { get; set; }
        public IEnumerable<Especialidade> Especialidades { get; set; }
    }
}