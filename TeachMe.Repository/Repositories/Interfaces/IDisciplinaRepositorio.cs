using System;
using System.Collections.Generic;
using TeachMe.Repository.Entities;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IDisciplinaRepositorio
    {
        List<Disciplina> ObterDisciplinas();
        Disciplina ObterDisciplinaPorId(Guid id);
    }
}
