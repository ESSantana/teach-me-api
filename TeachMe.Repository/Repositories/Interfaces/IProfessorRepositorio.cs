using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IProfessorRepositorio
    {
        List<Professor> ObterProfessores(long requisitanteId, long id = 0, string nome = null, string disciplina = null);
        Professor TornarProfessor(Professor professor);
        List<AvaliacaoProfessor> ObterAvaliacaoPorIdProfessor(long professorId);
    }
}
