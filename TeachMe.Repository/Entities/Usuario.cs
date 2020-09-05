using System;

namespace TeachMe.Repository.Entities
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Escolaridade { get; set; }
        public string TipoDocumento { get; set; }
        public string NuDocumento { get; set; }
        public Guid CargoId { get; set; }

        public virtual Cargo Cargo { get; set; }
    }
}
