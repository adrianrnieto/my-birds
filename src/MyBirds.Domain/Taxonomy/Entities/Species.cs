using MyBirds.Domain.Photography.Entities;

namespace MyBirds.Domain.Taxonomy.Entities;

public partial class Species : NamedEntity
{
    public required string ScientificName { get; set; }
    public required int GenusId { get; set; }
    public virtual Genus? Genus { get; set; }
    public virtual ICollection<Photo> Photos { get; set; } = [];
}
