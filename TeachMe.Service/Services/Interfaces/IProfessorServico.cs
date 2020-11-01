using System.Collections.Generic;
using TeachMe.Core.Dominio;
using TeachMe.Repository.Entities;

namespace TeachMe.Core.Services.Interfaces
{
    public interface IProfessorServico
    {
        List<Professor> ObterProfessores(long id = 0, string nome = null, string disciplina = null);
        Professor TornarProfessor(Professor professor, string email, string senha);
    }
}
