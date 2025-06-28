using MyBirds.Infrastructure.Database.Abstract;

namespace MyBirds.Infrastructure.Database.Entities;

internal partial class Genus : Entity
{
    public required string Name { get; set; }
    public required int FamilyId { get; set; }
    public virtual Family? Family { get; set; }
    public virtual ICollection<Species> Species { get; set; } = [];
}
