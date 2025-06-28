using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBirds.Infrastructure.Database.Entities;

namespace MyBirds.Infrastructure.Database.Contexts.Configurations;

internal partial class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_Family");

        entity.Property(e => e.Id)
            .UseIdentityColumn(1, 1);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);

        entity.HasOne(e => e.Order)
            .WithMany(e => e.Families)
            .HasForeignKey(f => f.OrderId)
            .HasConstraintName("FK_Order_Family");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Family> entity);
}
