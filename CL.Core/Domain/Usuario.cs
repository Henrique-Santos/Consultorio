using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Core.Domain
{
    public class Usuario
    {
        public Usuario()
        {
            Funcoes = new HashSet<Funcao>();
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public ICollection<Funcao> Funcoes { get; set; }
    }
}
