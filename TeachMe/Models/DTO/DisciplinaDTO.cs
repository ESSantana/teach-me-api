using System;

namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Disciplina
    /// </summary>
    public class DisciplinaDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Disciplina está ativa?
        /// </summary>
        public bool Ativo { get; set; }
    }
}
