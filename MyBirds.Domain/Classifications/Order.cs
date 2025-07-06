using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Classifications;

public partial class Order : NamedEntity
{
    public virtual ICollection<Family> Families { get; set; } = [];
}
