using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

public static class UsuarioMapper
{
    public static void Map(Profile profile)
    {
        profile.CreateMap<UsuarioDTO, Usuario>();
        profile.CreateMap<Usuario, UsuarioViewModel>();
    }
}