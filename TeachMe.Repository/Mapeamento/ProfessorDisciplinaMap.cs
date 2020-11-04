using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class ProfessorDisciplinaMap : IEntityTypeConfiguration<ProfessorDisciplina>
    {
        public void Configure(EntityTypeBuilder<ProfessorDisciplina> builder)
        {
            builder.ToTable("PROFESSOR_DISCIPLINA");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("PROFESSOR_DISCIPLINA_ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProfessorId)
                .HasColumnName("PROFESSOR_DADOS_ID");

            builder.Property(x => x.DisciplinaId)
                .HasColumnName("DISCIPLINA_ID");

            builder.HasOne(x => x.Professor)
                .WithMany(x => x.ProfessorDisciplina)
                .HasForeignKey(x => x.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Disciplina)
                .WithMany(x => x.ProfessorDisciplina)
                .HasForeignKey(x => x.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
