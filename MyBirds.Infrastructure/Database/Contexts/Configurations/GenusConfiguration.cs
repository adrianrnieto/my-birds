using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBirds.Infrastructure.Database.Entities;

namespace MyBirds.Infrastructure.Database.Contexts.Configurations;

internal partial class GenusConfiguration : IEntityTypeConfiguration<Genus>
{
    public void Configure(EntityTypeBuilder<Genus> entity)
    {
        entity.HasKey(e => e.Id)
            .HasName("PK_Genus");

        entity.Property(e => e.Id)
            .UseIdentityColumn(1, 1);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);

        entity.HasOne(e => e.Family)
            .WithMany(e => e.Genera)
            .HasForeignKey(f => f.FamilyId)
            .HasConstraintName("FK_Family_Genus");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Genus> entity);
}
