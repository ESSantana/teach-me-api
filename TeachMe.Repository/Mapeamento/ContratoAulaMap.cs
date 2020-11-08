using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class ContratoAulaMap : IEntityTypeConfiguration<ContratoAula>
    {
        public void Configure(EntityTypeBuilder<ContratoAula> builder)
        {
            builder.ToTable("CONTRATO_AULA");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.AvaliacaoProfessor);

            builder.Property(x => x.Id)
                .HasColumnName("CONTRATO_AULA_ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AlunoId)
                .HasColumnName("ALUNO_ID")
                .IsRequired();

            builder.Property(x => x.ProfessorId)
                .HasColumnName("PROFESSOR_ID")
                .IsRequired();

            builder.Property(x => x.DataContrato)
                .HasColumnName("DATA_CONTRATO");

            builder.Property(x => x.DataInicioPrestacao)
                .HasColumnName("DATA_INICIO_PRESTACAO");

            builder.Property(x => x.DataFimPrestacao)
                .HasColumnName("DATA_FIM_PRESTACAO");

            builder.Property(x => x.HorasContratadas)
               .HasColumnName("HORAS_CONTRATADAS");

            builder.Property(x => x.ValorHora)
                .HasColumnName("VALOR_AULA");

            builder.Property(x => x.ValorTotal)
                .HasColumnName("VALOR_TOTAL");

            builder.Property(x => x.Avaliado)
                .HasColumnName("AVALIADO")
                .IsRequired();

            builder.HasOne(x => x.Aluno)
                 .WithMany(x => x.ContratoAulas)
                 .HasForeignKey(x => x.AlunoId);

            builder.HasOne(x => x.Professor)
                .WithMany(x => x.ContratoAulas)
                .HasForeignKey(x => x.ProfessorId);
        }
    }
}
