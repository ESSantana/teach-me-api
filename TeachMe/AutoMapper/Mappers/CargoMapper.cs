using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class CargoMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<CargoDTO, Cargo>();
            profile.CreateMap<Cargo, CargoDTO>();
        }
    }
}
