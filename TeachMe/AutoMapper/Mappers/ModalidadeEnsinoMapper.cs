using AutoMapper;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class ModalidadeEnsinoMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<ModalidadeEnsino, ModalidadeEnsinoViewModel>();
        }
    }
}
