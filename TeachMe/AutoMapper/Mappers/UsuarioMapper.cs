using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class UsuarioMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<UsuarioDTO, Usuario>();
            profile.CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo.Descricao));
        }
    }
}