namespace MyBirds.Domain.Shared;

public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public int Id { get; set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
}
