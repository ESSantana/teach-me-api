using System;

namespace TeachMe.API.Models.DTO
{
    public class DisciplinaDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
