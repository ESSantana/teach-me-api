using System;

namespace TeachMe.Core.Dominio
{
    public class Escolaridade
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Professor Professor { get; set; }
    }
}
