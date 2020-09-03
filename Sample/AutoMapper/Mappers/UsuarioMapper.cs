using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Entities;

public static class UsuarioMapper
{
    public static void Map(Profile profile)
    {
        profile.CreateMap<UsuarioDTO, Usuario>();
        profile.CreateMap<Usuario, UsuarioDTO>();
    }
}