namespace TeachMe.Core.Dominio
{
    public class AvaliacaoProfessor
    {
        public long Id { get; set; }
        public long ProfessorId { get; set; }
        public long AulaId { get; set; }
        public int Nota { get; set; }
        public string Observacoes { get; set; }

        public Professor Professor { get; set; }
        public ContratoAula ContratoAula { get; set; }
    }
}
