using Confluent.Kafka;
using MyBirds.Domain.Shared;
using System.Text.Json;

namespace MyBirds.Infrastructure.Messaging;

internal class KafkaEventPublisher<TEvent>(IProducer<string, string> producer, string topic) : IEventPublisher<TEvent>
    where TEvent : IEvent
{
    private readonly IProducer<string, string> _producer = producer;
    private readonly string _topic = topic;

    public async Task PublishAsync(TEvent @event, CancellationToken cancellationToken)
    {
        var message = new Message<string, string>
        {
            Key = @event.Id.ToString(),
            Value = JsonSerializer.Serialize(@event)
        };

        await _producer.ProduceAsync(_topic, message, cancellationToken);
    }
}
