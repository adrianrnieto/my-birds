using MyBirds.Domain.Shared;

namespace MyBirds.Infrastructure.Messaging;

internal interface IDomainEventPublisher<TEvent>
    where TEvent : IDomainEvent
{
    Task PublishAsync(TEvent @event, CancellationToken cancellationToken);
}
