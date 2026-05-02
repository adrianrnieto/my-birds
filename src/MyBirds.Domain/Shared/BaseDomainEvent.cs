namespace MyBirds.Domain.Shared;

public abstract record BaseDomainEvent : IDomainEvent
{
    public Guid Id { get; }
    public DateTime CreationDate { get; }

    protected BaseDomainEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
}
