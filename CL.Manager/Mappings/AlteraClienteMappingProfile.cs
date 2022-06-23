using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using System;

namespace CL.Manager.Mappings;

public class AlteraClienteMappingProfile : Profile
{
    public AlteraClienteMappingProfile()
    {
        CreateMap<AlteraCliente, Cliente>()
            .ForMember(c => c.UltimaAtualizacao, o => o.MapFrom(x => DateTime.Now))
            .ForMember(c => c.DataNascimento, o => o.MapFrom(x => x.DataNascimento.Date));
    }
}
