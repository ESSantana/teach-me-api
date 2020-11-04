using System;

namespace TeachMe.Core.Dominio
{
    public class ContratoAula
    {
        public long Id { get; set; }
        public long AlunoId { get; set; }
        public long ProfessorId { get; set; }
        public DateTime? DataContrato { get; set; }
        public DateTime? DataInicioPrestacao { get; set; }
        public DateTime? DataFimPrestacao { get; set; }
        public int HorasContratadas { get; set; }
        public decimal ValorHora { get; set; }
        public bool Avaliado { get; set; }

        public virtual Usuario Aluno { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual AvaliacaoProfessor AvaliacaoProfessor { get; set; }
    }
}
