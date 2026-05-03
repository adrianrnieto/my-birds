using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Photography.Entities;

public partial class Photo : NamedEntity
{
    // TODO: Review whether this should be renamed to RelativePath instead
    public required string FullPath { get; set; }
    public int? CountryId { get; set; }
    public required int SpeciesId { get; set; }
    public virtual Species? Species { get; set; }
    // TODO: Move to a separate table to ensure only one photo can be set as favourite or starred per species
    public bool IsFavourite { get; set; }
    public bool IsStarred { get; set; }
}
