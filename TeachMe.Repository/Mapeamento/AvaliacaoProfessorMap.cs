using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class AvaliacaoProfessorMap : IEntityTypeConfiguration<AvaliacaoProfessor>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoProfessor> builder)
        {
            builder.ToTable("AVALIACAO_PROFESSOR");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("AVALIACAO_PROFESSOR_ID");

            builder.Property(x => x.ProfessorId)
                .IsRequired()
                .HasColumnName("PROFESSOR_ID");

            builder.Property(x => x.Nota)
                .IsRequired()
                .HasColumnName("NOTA");

            builder.Property(x => x.Observacoes)
                .HasColumnName("OBSERVACOES");

            builder.HasOne(x => x.Professor)
                .WithMany(x => x.AvaliacaoProfessor)
                .HasForeignKey(x => x.ProfessorId);

            builder.HasOne(x => x.ContratoAula)
                .WithOne(x => x.AvaliacaoProfessor)
                .HasForeignKey<AvaliacaoProfessor>(x => x.AulaId);
        }
    }
}
