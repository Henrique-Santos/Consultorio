using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Cliente;
using CL.Core.Shared.ModelViews.Endereco;
using CL.Core.Shared.ModelViews.Telefone;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Manager.Mappings
{
    public class NovoClienteMappingProfile : Profile
    {
        public NovoClienteMappingProfile()
        {
            CreateMap<NovoCliente, Cliente>()
                .ForMember(c => c.Criacao, o => o.MapFrom(x => DateTime.Now)) // Adicionando a propriedade Criacao da classe Cliente o valor obtido de DateTime.Now
                .ForMember(c => c.DataNascimento, o => o.MapFrom(x => x.DataNascimento.Date)); // Removendo a informação do tempo ao receber o parametro da DataNascimento

            CreateMap<NovoEndereco, Endereco>();

            CreateMap<NovoTelefone, Telefone>();
        }
    }
}
