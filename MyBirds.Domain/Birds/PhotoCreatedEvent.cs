using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

public sealed record PhotoCreatedEvent(int PhotoId) : BaseDomainEvent;
