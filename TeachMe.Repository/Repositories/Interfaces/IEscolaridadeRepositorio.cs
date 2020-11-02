using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IEscolaridadeRepositorio
    {
        List<Escolaridade> ObterTodasEscolaridades();
        Escolaridade ObterEscolaridadePorId(Guid Id);
    }
}
