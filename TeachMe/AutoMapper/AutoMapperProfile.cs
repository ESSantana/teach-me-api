using AutoMapper;
using TeachMe.API.AutoMapper.Mappers;

namespace TeachMe.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UsuarioMapper.Map(this);
            CargoMapper.Map(this);
            DisciplinaMapper.Map(this);
            ProfessorMapper.Map(this);
            EscolaridadeMapper.Map(this);
            ModalidadeEnsinoMapper.Map(this);
            ContratoAulaMapper.Map(this);
        }
    }
}