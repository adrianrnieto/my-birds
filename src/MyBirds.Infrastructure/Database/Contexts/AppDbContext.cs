using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts.Configurations;

namespace MyBirds.Infrastructure.Database.Contexts;

internal class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Family> Families { get; set; }
    public DbSet<Genus> Genera { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new FamilyConfiguration());
        modelBuilder.ApplyConfiguration(new GenusConfiguration());
        modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
        modelBuilder.ApplyConfiguration(new PhotoConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
