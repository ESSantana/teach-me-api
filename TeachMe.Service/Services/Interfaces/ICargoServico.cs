using System;
using System.Collections.Generic;
using TeachMe.Repository.Entities;

namespace TeachMe.Core.Services.Interfaces
{
    public interface ICargoServico
    {
        List<Cargo> ObterCargos();
        Cargo ObterCargoPorId(Guid id);
    }
}
