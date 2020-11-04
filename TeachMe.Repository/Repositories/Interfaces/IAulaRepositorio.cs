using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Repositories.Interfaces
{
    public interface IAulaRepositorio
    {
        ContratoAula ContratarAula(ContratoAula contrato);
        ContratoAula ObterAulaParaAvaliarPorId(long aulaId);
        List<ContratoAula> ObterAulaParaAvaliar(long alunoId);
        AvaliacaoProfessor AvaliarProfessor(AvaliacaoProfessor avaliacao);
        bool AulaParaAvaliacao(long alunoId, long professorId, long aulaId);
    }
}
