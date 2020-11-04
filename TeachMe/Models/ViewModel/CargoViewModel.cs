using System;

namespace TeachMe.API.Models.ViewModel
{
    /// <summary>
    /// Cargo
    /// </summary>
    public class CargoViewModel
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
        /// Cargo está ativo?
        /// </summary>
        public bool Ativo { get; set; }
    }
}
