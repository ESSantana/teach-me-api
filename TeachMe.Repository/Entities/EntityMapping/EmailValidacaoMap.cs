using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeachMe.Repository.Entities.EntityMapping
{
  public class EmailValidacaoMap : IEntityTypeConfiguration<EmailValidacao>
  {
    public void Configure(EntityTypeBuilder<EmailValidacao> builder)
    {
      builder.ToTable("EMAIL_VALIDACAO");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).HasColumnName("VALIDACAO_ID");
      builder.Property(x => x.UsuarioId).HasColumnName("USUARIO_ID");
      builder.Property(x => x.Valido).HasColumnName("VALIDO");
      builder.Property(x => x.DataValidacao).HasColumnName("DATA_VALIDACAO");

      builder.HasOne<Usuario>()
        .WithOne(x => x.EmailValidacao)
        .HasForeignKey<Usuario>(x => x.Id);
    }
  }
}