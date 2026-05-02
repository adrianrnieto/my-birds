using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBirds.Domain.Birds;

namespace MyBirds.Infrastructure.Database.Contexts.Configurations;

internal partial class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("PK_Photo");

        entity.Property(e => e.Id)
            .UseIdentityColumn(1, 1);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(128);

        entity.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("IX_Photo_Name");

        entity.HasOne(e => e.Species)
            .WithMany(e => e.Photos)
            .HasForeignKey(f => f.SpeciesId)
            .HasConstraintName("FK_Species_Photo");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Photo> entity);
}
