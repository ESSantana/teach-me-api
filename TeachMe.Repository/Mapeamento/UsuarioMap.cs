using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Disciplinas);
            builder.Ignore(x => x.ProfessorDisciplina);
            builder.Ignore(x => x.EmailValidacao);
            builder.Ignore(x => x.Professor);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(80);

            builder.Property(x => x.DataNascimento)
                .HasColumnName("DATA_NASCIMENTO");

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100);

            builder.Property(x => x.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(255);

            builder.Property(x => x.Telefone)
                .HasColumnName("TELEFONE")
                .HasMaxLength(11);

            builder.Property(x => x.Escolaridade)
                .HasColumnName("ESCOLARIDADE")
                .HasMaxLength(100);

            builder.Property(x => x.NuDocumento)
                .HasColumnName("NU_DOCUMENTO")
                .HasMaxLength(11);

            builder.Property(x => x.TipoDocumento)
                .HasColumnName("TIPO_DOCUMENTO")
                .HasMaxLength(3);

            builder.Property(x => x.CargoId)
                .HasColumnName("CARGO_ID")
                .HasMaxLength(36);

            builder.Property(x => x.UF)
                .HasColumnName("UF")
                .HasMaxLength(36);

            builder.Property(x => x.Cidade)
                .HasColumnName("CIDADE")
                .HasMaxLength(36);

            builder.HasOne(x => x.Cargo)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.CargoId);

            builder.HasMany(x => x.ContratoAulas)
                .WithOne(x => x.Aluno)
                .HasForeignKey(x => x.AlunoId);
        }
    }
}
