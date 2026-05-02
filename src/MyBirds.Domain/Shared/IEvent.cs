namespace MyBirds.Domain.Shared;

public interface IEvent
{
    public Guid Id { get; }
    public DateTime CreationDate { get; }
}
