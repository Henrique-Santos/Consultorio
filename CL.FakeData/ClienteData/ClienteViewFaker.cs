using Bogus;
using Bogus.Extensions.Brazil;
using CL.Core.Shared.ModelViews.Cliente;
using CL.FakeData.TelefoneData;
using CL.FakeData.EnderecoData;

namespace CL.FakeData.ClienteData;

public class ClienteViewFaker : Faker<ClienteView>
{
    public ClienteViewFaker()
    {
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(p => p.Id, x => id);
        RuleFor(p => p.Nome, x => x.Person.FullName);
        RuleFor(p => p.Sexo, x => x.PickRandom<SexoView>());
        RuleFor(p => p.Documento, x => x.Person.Cpf());
        RuleFor(p => p.Criacao, x => x.Date.Past());
        RuleFor(p => p.UltimaAtualizacao, x => x.Date.Past());
        RuleFor(p => p.DataNascimento, x => x.Date.Past());
        RuleFor(p => p.Telefones, x => new TelefoneViewFaker().Generate(3));
        RuleFor(p => p.Endereco, x => new EnderecoViewFaker().Generate());
    }
}
