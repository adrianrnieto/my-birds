namespace MyBirds.Domain.Shared;

public abstract class NamedEntity : Entity
{
    public required string Name { get; set; }
}
