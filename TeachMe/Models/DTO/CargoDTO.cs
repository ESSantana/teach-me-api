using System;

namespace TeachMe.API.Models.DTO
{
    public class CargoDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
