using System;
using System.Collections.Generic;

namespace TeachMe.Repository.Entities
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
