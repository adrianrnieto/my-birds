using MyBirds.Domain.Birds;
using MyBirds.Infrastructure.Messaging;

namespace MyBirds.Infrastructure.Events.Publishers;

internal class PhotoCreatedEventPublisher(IEventPublisher<PhotoCreatedEvent> kafkaPublisher)
{
    private readonly IEventPublisher<PhotoCreatedEvent> _kafkaPublisher = kafkaPublisher;

    public Task PublishAsync(PhotoCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        return _kafkaPublisher.PublishAsync(@event, cancellationToken);
    }
}
