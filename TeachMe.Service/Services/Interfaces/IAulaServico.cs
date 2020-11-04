using System.Collections.Generic;
using TeachMe.Core.Dominio;

namespace TeachMe.Service.Services.Interfaces
{
    public interface IAulaServico
    {
        ContratoAula ContratarAula(ContratoAula contrato);
        ContratoAula ObterAulaParaAvaliarPorId(long aulaId);
        List<ContratoAula> ObterAulaParaAvaliar(long alunoId);
        AvaliacaoProfessor AvaliarProfessor(AvaliacaoProfessor avaliacao, long alunoId);
    }
}
