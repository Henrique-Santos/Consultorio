using Bogus;
using CL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.FakeData.TelefoneData
{
    public class TelefoneFaker : Faker<Telefone>
    {
        public TelefoneFaker(int clientId)
        {
            RuleFor(o => o.ClienteId, f => clientId);
            RuleFor(o => o.Numero, f => f.Person.Phone);
        }
    }
}
