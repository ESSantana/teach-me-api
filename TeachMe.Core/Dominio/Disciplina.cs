using System;
using System.Collections.Generic;

namespace TeachMe.Core.Dominio
{
    public class Disciplina
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public virtual List<ProfessorDisciplina> ProfessorDisciplina { get; set; }
    }
}
