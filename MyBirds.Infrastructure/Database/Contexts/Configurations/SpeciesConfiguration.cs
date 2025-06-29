using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBirds.Domain.Birds;

namespace MyBirds.Infrastructure.Database.Contexts.Configurations;

internal partial class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("PK_Species");

        entity.Property(e => e.Id)
            .UseIdentityColumn(1, 1);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);

        entity.Property(e => e.ScientificName)
            .IsRequired()
            .HasMaxLength(128);

        entity.HasOne(e => e.Genus)
            .WithMany(e => e.Species)
            .HasForeignKey(f => f.GenusId)
            .HasConstraintName("FK_Genus_Species");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Species> entity);
}
