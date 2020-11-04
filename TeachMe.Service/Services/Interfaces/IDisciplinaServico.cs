using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Service.Services.Interfaces
{
    public interface IDisciplinaServico
    {
        List<Disciplina> ObterDisciplinas();
        Disciplina ObterDisciplinaPorId(Guid id);
    }
}
