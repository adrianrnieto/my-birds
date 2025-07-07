using MyBirds.Domain.Shared;

namespace Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static async Task<IEnumerable<string>> GetMissingByNamesAsync<TEntity>(this DbSet<TEntity> dbSet, IEnumerable<string> names, CancellationToken cancellationToken)
        where TEntity : NamedEntity
    {
        var existingPaths = await dbSet
            .AsNoTracking()
            .Where(p => names.Contains(p.Name))
            .Select(p => p.Name)
            .ToListAsync(cancellationToken);

        return names.Except(existingPaths);
    }
}
