using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

public partial class Photo : NamedEntity
{
    public required string FullPath { get; set; }
    public int? CountryId { get; set; }
    public required int SpeciesId { get; set; }
    public virtual Species? Species { get; set; }
    public bool IsFavourite { get; set; }
    public bool IsStarred { get; set; }
}
