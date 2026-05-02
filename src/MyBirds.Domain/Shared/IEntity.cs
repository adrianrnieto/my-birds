namespace MyBirds.Domain.Shared;

public interface IEntity
{
    int Id { get; }
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
