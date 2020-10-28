using System;

namespace TeachMe.Repository.Entities
{
    public class EmailValidacao
    {
        public Guid Id { get; set; }
        public bool Valido { get; set; }
        public DateTime DataValidacao { get; set; }
        public long UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}