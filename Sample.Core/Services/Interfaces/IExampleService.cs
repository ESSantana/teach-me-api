using TeachMe.Core.Entities;
using System.Collections.Generic;

namespace TeachMe.Core.Services.Interfaces
{
    public interface IExampleService
    {
        List<Usuario> Get();
        Usuario Get(long Id);
        int Create(List<Usuario> entities);
        Usuario Modify(Usuario entity);
        int Delete(long Id);
    }
}
