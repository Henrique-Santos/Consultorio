using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Core.Shared.ModelViews.Telefone
{
    public class TelefoneView : ICloneable
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
