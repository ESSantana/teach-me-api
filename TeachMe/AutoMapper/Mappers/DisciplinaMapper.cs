using AutoMapper;
using TeachMe.API.Models.DTO;
using TeachMe.Repository.Entities;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class DisciplinaMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<DisciplinaDTO, Disciplina>();
            profile.CreateMap<Disciplina, DisciplinaDTO>();
        }
    }
}
