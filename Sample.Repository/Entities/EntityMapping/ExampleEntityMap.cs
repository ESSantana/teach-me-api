using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachMe.Core.Entities;
using TeachMe.Repository.Context;

namespace TeachMe.Repository.Entities.EntityMapping
{
    public class ExampleEntityMap : IEntityTypeConfiguration<Usuario>
    {
        public readonly DbOptions _dbOptions;

        public ExampleEntityMap(DbOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Example");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(255);

        }
    }
}
