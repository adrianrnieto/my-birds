using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBirds.Infrastructure.Database.Contexts;

namespace Microsoft.AspNetCore.Builder;

public static class EntityFrameworkWebApplicationExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration["Database:ConnectionString"], x => x.MigrationsAssembly("MyBirds.Migrations"));
            //options.LogTo(Console.WriteLine);
        });
    }

    public static void UseEntityFrameworkMigrations(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}
