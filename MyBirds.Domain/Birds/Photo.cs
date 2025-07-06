using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

public partial class Photo : NamedEntity
{
    public int? CountryId { get; set; }
    public required int SpeciesId { get; set; }
    public virtual Species? Species { get; set; }
}
