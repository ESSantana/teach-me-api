namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Avaliacao Professor
    /// </summary>
    public class AvaliacaoProfessorDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id do Professor
        /// </summary>
        public long ProfessorId { get; set; }
        /// <summary>
        /// Id do Aluno
        /// </summary>
        public long AlunoId { get; set; }
        /// <summary>
        /// Id da Aula
        /// </summary>
        public long AulaId { get; set; }
        /// <summary>
        /// Nota do professor
        /// </summary>
        public int Nota { get; set; }
        /// <summary>
        /// Observações sobre o professor
        /// </summary>
        public string Observacoes { get; set; }
    }
}
