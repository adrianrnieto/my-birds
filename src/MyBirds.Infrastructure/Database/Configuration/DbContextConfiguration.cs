using Microsoft.EntityFrameworkCore;

namespace MyBirds.Infrastructure.Database.Configuration;

internal static class DbContextConfiguration
{
    public static void ConfigureDbContextOptions(this DbContextOptionsBuilder options, IConfiguration configuration)
    {
        var connectionString = configuration["Database:ConnectionString"];
        options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MyBirds.Migrations"));
    }
}
