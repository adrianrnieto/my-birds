using MyBirds.Domain.Shared;

namespace MyBirds.Infrastructure.Messaging;

internal class DomainEventPublisher<TEvent>(IEventPublisher<TEvent> kafkaPublisher)
    : IDomainEventPublisher<TEvent>
    where TEvent : IDomainEvent
{
    private readonly IEventPublisher<TEvent> _kafkaPublisher = kafkaPublisher;

    public Task PublishAsync(TEvent @event, CancellationToken cancellationToken)
    {
        return _kafkaPublisher.PublishAsync(@event, cancellationToken);
    }
}
