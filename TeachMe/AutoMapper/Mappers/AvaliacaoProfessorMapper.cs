using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class AvaliacaoProfessorMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<AvaliacaoProfessorDTO, AvaliacaoProfessor>();
            profile.CreateMap<AvaliacaoProfessor, AvaliacaoProfessorViewModel>();
        }
    }
}
