namespace MyBirds.Domain.Taxonomy.Entities;

public partial class Order : NamedEntity
{
    public virtual ICollection<Family> Families { get; set; } = [];
}
