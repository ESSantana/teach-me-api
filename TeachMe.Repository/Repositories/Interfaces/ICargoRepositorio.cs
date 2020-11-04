using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface ICargoRepositorio
    {
        List<Cargo> ObterCargos();
        Cargo ObterCargoPorId(Guid id);
    }
}
