﻿using Bogus;
using Bogus.Extensions.Brazil;
using CL.Core.Shared.ModelViews.Endereco;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.FakeData.ClienteData
{
    public class NovoEnderecoFaker : Faker<NovoEndereco>
    {
        public NovoEnderecoFaker()
        {
            RuleFor(p => p.Numero, x => x.Address.BuildingNumber());
            RuleFor(p => p.CEP, x => Convert.ToInt32(x.Address.ZipCode().Replace("-", "")));
            RuleFor(p => p.Cidade, x => x.Address.City());
            RuleFor(p => p.Estado, x => x.PickRandom<EstadoView>());
            RuleFor(p => p.Logradouro, x => x.Address.StreetName());
            RuleFor(p => p.Complemento, x => x.Lorem.Sentence(20));
        }
    }
}
