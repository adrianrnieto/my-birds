namespace MyBirds.Domain.Classifications;

public interface IFamilyWriteRepository
{
    Task AddAsync(IEnumerable<Family> families, CancellationToken cancellationToken);
}
