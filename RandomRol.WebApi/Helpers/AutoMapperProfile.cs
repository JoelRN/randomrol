using AutoMapper;
using RandomRol.WebApi.Dtos;
using RandomRol.WebApi.Entities;

namespace RandomRol.WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}