using MyBirds.Domain.Birds;
using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Classifications;

public partial class Genus : NamedEntity
{
    public required int FamilyId { get; set; }
    public virtual Family? Family { get; set; }
    public virtual ICollection<Species> Species { get; set; } = [];
}
