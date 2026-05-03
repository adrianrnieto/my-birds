namespace MyBirds.Domain.Taxonomy.Entities;

public partial class Genus : NamedEntity
{
    public required int FamilyId { get; set; }
    public virtual Family? Family { get; set; }
    public virtual ICollection<Species> Species { get; set; } = [];
}
