using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelViews.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace CL.Manager.Mappings
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioView>().ReverseMap();
            CreateMap<Usuario, NovoUsuario>().ReverseMap();
            CreateMap<Usuario, UsuarioLogado>().ReverseMap();
            CreateMap<Funcao, FuncaoView>().ReverseMap();
            CreateMap<Funcao, ReferenciaFuncao>().ReverseMap();
        }
    }
}
