using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Classifications;

public partial class Family : Entity
{
    public required string Name { get; set; }
    public required int OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public virtual ICollection<Genus> Genera { get; set; } = [];
}
