using MyBirds.Application.Abstract;

namespace MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;

public record RegisterPhotosAndTaxonomyCommand : ICommand
{
    public required IEnumerable<string> PhotoPaths { get; init; }
}
