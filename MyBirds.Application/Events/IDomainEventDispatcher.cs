using MyBirds.Domain.Shared;

namespace MyBirds.Application.Events;

public interface IDomainEventDispatcher
{
    Task DispatchDomainEventsAsync(IEnumerable<IEntity> entities, CancellationToken cancellationToken);
}
