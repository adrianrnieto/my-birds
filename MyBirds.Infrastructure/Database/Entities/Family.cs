using MyBirds.Infrastructure.Database.Abstract;

namespace MyBirds.Infrastructure.Database.Entities;

internal partial class Family : Entity
{
    public required string Name { get; set; }
    public required int OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public virtual ICollection<Genus> Genera { get; set; } = [];
}
