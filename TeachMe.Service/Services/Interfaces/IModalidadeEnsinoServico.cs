using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Service.Services.Interfaces
{
    public interface IModalidadeEnsinoServico
    {
        List<ModalidadeEnsino> ObterTodasModalidades();
        ModalidadeEnsino ObterModalidadePorId(Guid Id);
    }
}
