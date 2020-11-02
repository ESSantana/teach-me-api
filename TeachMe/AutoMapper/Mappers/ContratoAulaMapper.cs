using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class ContratoAulaMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<ContratoAulaDTO, ContratoAula>();
            profile.CreateMap<ContratoAula, ContratoAulaViewModel>();
        }
    }
}
