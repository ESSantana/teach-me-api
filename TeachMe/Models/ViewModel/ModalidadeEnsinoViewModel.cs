using System;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Modalidade de Ensino
    /// </summary>
    public class ModalidadeEnsinoViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }
    }
}
