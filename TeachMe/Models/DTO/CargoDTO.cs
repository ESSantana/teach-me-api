using System;

namespace TeachMe.API.Models.DTO
{
    /// <summary>
    /// Cargo
    /// </summary>
    public class CargoDTO
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
