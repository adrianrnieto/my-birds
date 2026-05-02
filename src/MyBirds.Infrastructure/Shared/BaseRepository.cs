using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Shared;

internal abstract class BaseRepository(AppDbContext appDbContext)
{
    protected readonly AppDbContext _appDbContext = appDbContext;
}
