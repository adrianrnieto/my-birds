namespace MyBirds.Domain.Classifications.Repositories;

public interface IFamilyWriteRepository
{
    Task AddAsync(IEnumerable<Family> families, CancellationToken cancellationToken);
}
