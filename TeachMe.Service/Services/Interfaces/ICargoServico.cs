using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Core.Services.Interfaces
{
    public interface ICargoServico
    {
        List<Cargo> ObterCargos();
        Cargo ObterCargoPorId(Guid id);
    }
}
