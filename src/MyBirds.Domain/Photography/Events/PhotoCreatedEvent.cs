namespace MyBirds.Domain.Photography.Events;

public sealed record PhotoCreatedEvent(int PhotoId) : BaseDomainEvent;
