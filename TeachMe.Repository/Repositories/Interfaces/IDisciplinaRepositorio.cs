using System;
using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IDisciplinaRepositorio
    {
        List<Disciplina> ObterDisciplinas();
        Disciplina ObterDisciplinaPorId(Guid id);
    }
}
