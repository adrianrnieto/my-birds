using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

public sealed record PhotosCreatedBatchEvent(IEnumerable<int> PhotoIds) : BaseDomainEvent;
