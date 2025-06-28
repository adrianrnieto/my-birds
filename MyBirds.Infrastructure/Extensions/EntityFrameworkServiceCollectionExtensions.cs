using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBirds.Infrastructure.Database.Contexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration["Database:ConnectionString"], x => x.MigrationsAssembly("MyBirds.Migrations"));
            //options.LogTo(Console.WriteLine);
        });
    }
}
