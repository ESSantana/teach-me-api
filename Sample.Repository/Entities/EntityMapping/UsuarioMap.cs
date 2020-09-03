using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Entities;
using TeachMe.Repository.Context;

namespace TeachMe.Repository.Entities.EntityMapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public readonly DbOptions _dbOptions;

        public UsuarioMap(DbOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50);

            builder.Property(x => x.DataNascimento)
                .HasColumnName("DATA_NASCIMENTO")
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(255);

            builder.Property(x => x.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(255);

            builder.Property(x => x.Telefone)
                .HasColumnName("TELEFONE")
                .HasMaxLength(255);

            builder.Property(x => x.Escolaridade)
                .HasColumnName("ESCOLARIDADE")
                .HasMaxLength(255);

            builder.Property(x => x.NuDocumento)
                .HasColumnName("NU_DOCUMENTO")
                .HasMaxLength(255);

            builder.Property(x => x.TipoDocumento)
                .HasColumnName("TIPO_DOCUMENTO")
                .HasMaxLength(255);
        }
    }
}
