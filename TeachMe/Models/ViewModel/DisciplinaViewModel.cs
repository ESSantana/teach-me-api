using System;

namespace TeachMe.API.Models.ViewModel
{
    public class DisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
