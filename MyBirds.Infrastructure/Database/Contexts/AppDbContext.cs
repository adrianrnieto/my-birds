using Microsoft.EntityFrameworkCore;
using MyBirds.Infrastructure.Database.Contexts.Configurations;
using MyBirds.Infrastructure.Database.Entities;

namespace MyBirds.Infrastructure.Database.Contexts;

internal class AppDbContext : DbContext
{
    public DbSet<Bird> Birds { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BirdConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
