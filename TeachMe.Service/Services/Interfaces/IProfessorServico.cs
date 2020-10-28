using System.Collections.Generic;
using TeachMe.Repository.Entities;

namespace TeachMe.Core.Services.Interfaces
{
    public interface IProfessorServico
    {
        List<Usuario> ObterProfessores(long id = 0, string nome = null, string disciplina = null);
    }
}
