using MediatR;

namespace MyBirds.Domain.Shared;

public interface IDomainEvent : INotification, IEvent
{
}
