using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBirds.Infrastructure.Database.Contexts;

namespace Microsoft.Extensions.Hosting;

public static class HostExtensions
{
    public static void UseEntityFrameworkMigrations(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}
