using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class ProfessorMap : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("PROFESSOR_DADOS");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Disciplinas);
            builder.Ignore(x => x.ProfessorDisciplina);

            builder.Property(x => x.Id)
                .HasColumnName("PROFESSOR_DADOS_ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UsuarioId)
                .HasColumnName("USUARIO_ID");

            builder.Property(x => x.ModalidadeEnsinoId)
                .HasColumnName("MODALIDADE_ENSINO_ID");

            builder.Property(x => x.EscolaridaPubAlvoId)
                .HasColumnName("ESCOLARIDADE_ID");

            builder.Property(x => x.Descricao)
                .HasColumnName("DESCRICAO");

            builder.Property(x => x.ValorHora)
                .HasColumnName("VALOR_HORA")
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithOne(x => x.Professor)
                .HasForeignKey<Professor>(x => x.UsuarioId);

            builder.HasOne(x => x.ModalidadeEnsino)
                .WithOne(x => x.Professor)
                .HasForeignKey<Professor>(x => x.ModalidadeEnsinoId);

            builder.HasOne(x => x.EscolaridaPubAlvo)
                .WithOne(x => x.Professor)
                .HasForeignKey<Professor>(x => x.EscolaridaPubAlvoId);

            builder.HasMany(x => x.ContratoAulas)
                .WithOne(x => x.Professor)
                .HasForeignKey(x => x.ProfessorId);

            builder.HasMany(x => x.AvaliacaoProfessor)
                .WithOne(x => x.Professor)
                .HasForeignKey(x => x.ProfessorId);
        }
    }
}
