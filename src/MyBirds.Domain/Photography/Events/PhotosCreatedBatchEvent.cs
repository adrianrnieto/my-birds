namespace MyBirds.Domain.Photography.Events;

public sealed record PhotosCreatedBatchEvent(IEnumerable<int> PhotoIds) : BaseDomainEvent;
