using System;

namespace TeachMe.Core.Dominio
{
    public class ProfessorDisciplina
    {
        public long Id { get; set; }
        public long ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}
