using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class EscolaridadeMap : IEntityTypeConfiguration<Escolaridade>
    {
        public void Configure(EntityTypeBuilder<Escolaridade> builder)
        {
            builder.ToTable("ESCOLARIDADE");

            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Professor);

            builder.Property(x => x.Id)
                .HasColumnName("ESCOLARIDADE_ID")
                .IsRequired();
            builder.Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(50);
        }
    }
}
