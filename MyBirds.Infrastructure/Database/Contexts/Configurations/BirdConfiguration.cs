using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBirds.Infrastructure.Database.Entities;

namespace MyBirds.Infrastructure.Database.Contexts.Configurations;

internal partial class BirdConfiguration : IEntityTypeConfiguration<Bird>
{
    public void Configure(EntityTypeBuilder<Bird> entity)
    {
        entity.HasKey(e => e.Id).HasName("Bird_PK");

        entity.Property(e => e.Id).UseIdentityColumn();
        entity.Property(e => e.Name).IsRequired();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Bird> entity);
}