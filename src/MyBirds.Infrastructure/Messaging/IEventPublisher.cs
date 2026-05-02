namespace MyBirds.Infrastructure.Messaging;

internal interface IEventPublisher<in TEvent>
{
    Task PublishAsync(TEvent @event, CancellationToken cancellationToken);
}
