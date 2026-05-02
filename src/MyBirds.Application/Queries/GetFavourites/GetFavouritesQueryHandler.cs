using MyBirds.Application.Abstract;
using MyBirds.Domain.Classifications;

namespace MyBirds.Application.Queries.GetFavourites;

public class GetFavouritesQueryHandler(IPhotoReadRepository photoReadRepository, ISpeciesReadRepository speciesReadRepository)
    : IAsyncQueryHandler<GetFavouritesQueryResult>
{
    public async Task<Result<GetFavouritesQueryResult>> HandleAsync(CancellationToken cancellationToken)
    {
        var species = await speciesReadRepository.GetAllAsync(cancellationToken);
        var photos = await photoReadRepository.GetFavouritesAsync(cancellationToken);
        var resultValue = new GetFavouritesQueryResult
        {
            SpeciesAndPhotosData = species.Select(sp => (Species: sp, Photo: photos.SingleOrDefault(p => p.SpeciesId == sp.Id)))
        };
        return Result<GetFavouritesQueryResult>.Success(resultValue);
    }
}
