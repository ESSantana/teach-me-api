using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IModalidadeEnsinoRepositorio
    {
        List<ModalidadeEnsino> ObterTodasModalidades();
        ModalidadeEnsino ObterModalidadePorId(Guid Id);
    }
}
