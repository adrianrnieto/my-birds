using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyBirds.Infrastructure.Database.Contexts;
using System.Text.Json;

namespace MyBirds.Migrations;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (!File.Exists(appSettingsPath))
        {
            throw new FileNotFoundException($"appsettings.json not found at {appSettingsPath}");
        }

        var json = File.ReadAllText(appSettingsPath);
        var config = JsonSerializer.Deserialize<JsonElement>(json);

        string connectionString = config.GetProperty("Database").GetProperty("ConnectionString").GetString()
            ?? throw new InvalidOperationException("Database:ConnectionString not found in appsettings.json");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("MyBirds.Migrations"));

        return new AppDbContext(optionsBuilder.Options);
    }
}
