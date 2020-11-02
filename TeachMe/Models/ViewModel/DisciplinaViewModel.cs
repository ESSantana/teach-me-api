using System;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Disciplina
    /// </summary>
    public class DisciplinaViewModel
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
