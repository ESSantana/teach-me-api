using TeachMe.Core.Entities;
using System.Collections.Generic;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IExampleRepository
    {
        List<Usuario> Get();
        Usuario Get(long Id);
        int Create(List<Usuario> entity);
        Usuario Modify(Usuario entity);
        int Delete(long Id);
    }
}
