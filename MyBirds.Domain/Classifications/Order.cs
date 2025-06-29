using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Classifications;

public partial class Order : Entity
{
    public required string Name { get; set; }
    public virtual ICollection<Family> Families { get; set; } = [];
}
