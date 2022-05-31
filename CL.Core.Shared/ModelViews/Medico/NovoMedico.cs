using CL.Core.Shared.ModelViews.Especialidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Core.Shared.ModelViews.Medico
{
    public class NovoMedico
    {
        public string Nome { get; set; }
        public int CRM { get; set; }
        public ICollection<ReferenciaEspecialidade> Especialidades { get; set; }
    }
}
