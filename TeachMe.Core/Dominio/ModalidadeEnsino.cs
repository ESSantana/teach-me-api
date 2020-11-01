using System;

namespace TeachMe.Core.Dominio
{
    public class ModalidadeEnsino
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public Professor Professor { get; set; }
    }
}
