using System;
using System.Collections.Generic;

namespace TeachMe.Repository.Entities
{
    public class ProfessorDisciplina
    {
        public long Id { get; set; }
        public long ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }

        public virtual Usuario Professor { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}
