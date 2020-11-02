using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Entities.EntityMapping
{
    public class DisciplinaMap : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("DISCIPLINA");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.ProfessorDisciplina);

            builder.Property(x => x.Id)
                .HasColumnName("DISCIPLINA_ID");

            builder.Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(50);

            builder.Property(x => x.Ativo)
                .HasColumnName("ATIVO");
        }
    }
}
