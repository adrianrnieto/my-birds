namespace MyBirds.Domain.Taxonomy.Entities;

public partial class Family : NamedEntity
{
    public required int OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public virtual ICollection<Genus> Genera { get; set; } = [];
}
