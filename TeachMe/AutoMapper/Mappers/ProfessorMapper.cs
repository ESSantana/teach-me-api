using AutoMapper;
using TeachMe.API.Models.ViewModel;
using TeachMe.Repository.Entities;

namespace TeachMe.API.AutoMapper.Mappers
{
    public static class ProfessorMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Usuario, ProfessorViewModel>();
        }
    }
}
