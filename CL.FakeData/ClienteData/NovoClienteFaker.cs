using Bogus;
using Bogus.Extensions.Brazil;
using CL.Core.Shared.ModelViews.Cliente;
using CL.FakeData.EnderecoData;
using CL.FakeData.TelefoneData;

namespace CL.FakeData.ClienteData;

public class NovoClienteFaker : Faker<NovoCliente>
{
    public NovoClienteFaker()
    {
        RuleFor(p => p.Nome, x => x.Person.FullName);
        RuleFor(p => p.Sexo, x => x.PickRandom<SexoView>());
        RuleFor(p => p.Documento, x => x.Person.Cpf());
        RuleFor(p => p.DataNascimento, x => x.Date.Past());
        RuleFor(p => p.Telefones, x => new NovoTelefoneFaker().Generate(3));
        RuleFor(p => p.Endereco, x => new NovoEnderecoFaker().Generate());
    }
}
