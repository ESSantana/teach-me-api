using AutoMapper;

namespace TeachMe.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UsuarioMapper.Map(this);
        }
    }
}