using Bogus;
using CL.Core.Shared.ModelViews.Telefone;

namespace CL.FakeData.TelefoneData;

public class TelefoneViewFaker : Faker<TelefoneView>
{
    public TelefoneViewFaker()
    {
        RuleFor(p => p.Id, x => x.Random.Number(1, 10));
        RuleFor(p => p.Numero, x => x.Person.Phone);
    }
}
