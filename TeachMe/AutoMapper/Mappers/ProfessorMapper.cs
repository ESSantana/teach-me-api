using System.Linq;
using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.API.Models.ViewModel;
using TeachMe.Core.Dominio;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class ProfessorMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<ProfessorDTO, Professor>();
            profile.CreateMap<Professor, ProfessorViewModel>()
                .ForMember(x => x.QtdAvaliacoes, opt => opt.MapFrom(
                    src => src.AvaliacaoProfessor.Count
                ))
                .ForMember(x => x.NotaMedia, opt =>
                {
                    opt.PreCondition(x => x.AvaliacaoProfessor != null && x.AvaliacaoProfessor.Count > 0);
                    opt.MapFrom(src => src.AvaliacaoProfessor.Sum(x => x.Nota) / src.AvaliacaoProfessor.Count);
                });
        }
    }
}
