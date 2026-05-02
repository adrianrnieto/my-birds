using MyBirds.Application.Abstract;
using MyBirds.Domain.Classifications;

namespace MyBirds.Application.Queries.GetPhotosBySpecies;

public class GetPhotosBySpeciesQueryHandler(IPhotoReadRepository photoReadRepository)
    : IAsyncQueryHandler<GetPhotosBySpeciesQuery, GetPhotosBySpeciesQueryResult>
{
    public async Task<Result<GetPhotosBySpeciesQueryResult>> HandleAsync(GetPhotosBySpeciesQuery query, CancellationToken cancellationToken)
    {
        var photos = await photoReadRepository.GetBySpeciesIdAsync(query.SpeciesId, cancellationToken);
        return Result<GetPhotosBySpeciesQueryResult>.Success(new GetPhotosBySpeciesQueryResult(photos));
    }
}
