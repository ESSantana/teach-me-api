namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Avaliacao Professor
    /// </summary>
    public class AvaliacaoProfessorViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id do Professor
        /// </summary>
        public int ProfessorId { get; set; }
        /// <summary>
        /// Id da Aula
        /// </summary>
        public long AulaId { get; set; }
        /// <summary>
        /// Aluno que fez a avaliação
        /// </summary>
        public string Aluno { get; set; }
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
