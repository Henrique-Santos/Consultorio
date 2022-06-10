using Bogus;
using CL.Core.Shared.ModelViews.Telefone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.FakeData.TelefoneData
{
    public class TelefoneViewFaker : Faker<TelefoneView>
    {
        public TelefoneViewFaker()
        {
            RuleFor(p => p.Id, x => x.Random.Number(1, 10));
            RuleFor(p => p.Numero, x => x.Person.Phone);
        }
    }
}
