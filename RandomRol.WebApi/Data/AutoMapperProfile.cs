using AutoMapper;
using RandomRol.WebApi.Dtos;
using RandomRol.WebApi.Entities;

namespace RandomRol.WebApi.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuarios, UsuarioDto>();
            CreateMap<UsuarioDto, Usuarios>();
        }
    }
}