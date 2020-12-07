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
            profile.CreateMap<AvaliacaoProfessor, AvaliacaoProfessorViewModel>()
                .ForMember(dest => dest.Aluno, opt => 
                {
                    opt.PreCondition(x => x.ContratoAula != null && x.ContratoAula.Aluno != null);
                    opt.MapFrom(x => x.ContratoAula.Aluno.Nome);
                });
        }
    }
}
