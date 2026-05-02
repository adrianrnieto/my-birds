using MediatR;
using MyBirds.Application.Events;
using MyBirds.Domain.Shared;

namespace MyBirds.Infrastructure.Events;

internal class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    private readonly IMediator _mediator = mediator;

    // TODO: Publish messages of the same type as batches
    public async Task DispatchDomainEventsAsync(IEnumerable<IEntity> entities, CancellationToken cancellationToken)
    {
        var domainEvents = entities.SelectMany(a => a.DomainEvents);

        foreach (var entity in entities)
        {
            entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
   