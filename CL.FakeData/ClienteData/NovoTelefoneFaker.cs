using Bogus;
using CL.Core.Shared.ModelViews.Telefone;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.FakeData.ClienteData
{
    public class NovoTelefoneFaker : Faker<NovoTelefone>
    {
        public NovoTelefoneFaker()
        {
            RuleFor(p => p.Numero, x => x.Person.Phone);
        }
    }
}
