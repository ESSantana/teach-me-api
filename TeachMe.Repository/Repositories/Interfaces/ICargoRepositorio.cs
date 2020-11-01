using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Entities;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface ICargoRepositorio
    {
        List<Cargo> ObterCargos();
        Cargo ObterCargoPorId(Guid id);
    }
}
