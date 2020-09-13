using System.Collections.Generic;
using TeachMe.Repository.Entities;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IProfessorRepositorio
    {
        List<Usuario> ObterProfessores(long id = 0, string nome = null, string disciplina = null);
    }
}
