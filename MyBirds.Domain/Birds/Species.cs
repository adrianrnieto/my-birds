using MyBirds.Domain.Classifications;
using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

public partial class Species : Entity
{
    public required string Name { get; set; }
    public required string ScientificName { get; set; }
    public required int GenusId { get; set; }
    public virtual Genus? Genus { get; set; }
    public virtual ICollection<Photo> Photos { get; set; } = [];
}
