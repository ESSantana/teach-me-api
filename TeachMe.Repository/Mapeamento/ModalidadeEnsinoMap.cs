using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Dominio;

namespace TeachMe.Repository.Mapeamento
{
    public class ModalidadeEnsinoMap : IEntityTypeConfiguration<ModalidadeEnsino>
    {
        public void Configure(EntityTypeBuilder<ModalidadeEnsino> builder)
        {
            builder.ToTable("MODALIDADE_ENSINO");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Professor);

            builder.Property(x => x.Id)
                .HasColumnName("MODALIDADE_ENSINO_ID")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(50);
        }
    }
}
