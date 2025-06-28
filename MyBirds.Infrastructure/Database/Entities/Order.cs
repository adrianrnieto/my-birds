using MyBirds.Infrastructure.Database.Abstract;

namespace MyBirds.Infrastructure.Database.Entities;

internal partial class Order : Entity
{
    public required string Name { get; set; }
    public virtual ICollection<Family> Families { get; set; } = [];
}
