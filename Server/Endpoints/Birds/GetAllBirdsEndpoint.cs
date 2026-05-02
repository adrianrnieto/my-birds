using FastEndpoints;
using MyBirds.Server.Models;
using MyBirds.Server.Services;
using MyBirds.Shared;

namespace MyBirds.Server.Endpoints.Birds;

public class GetAllBirdsEndpoint(IBirdsService birdsService) : EndpointWithoutRequest<IEnumerable<OrderGroupViewModel>>
{
    public override void Configure()
    {
        Get("/birds");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var birds = await birdsService.GetAll(ct);
        Response = GetViewModel(birds).ToList();
        await Task.CompletedTask;
    }

    private static IEnumerable<OrderGroupViewModel> GetViewModel(IEnumerable<BirdSpecies> birds)
    {
        return birds
            .GroupBy(b => b.Order)
            .Select(orderGroup => new OrderGroupViewModel
            {
                Order = orderGroup.Key,
                Families = orderGroup
                    .GroupBy(b => b.Family)
                    .Select(familyGroup => new FamilyGroupViewModel
                    {
                        Family = familyGroup.Key,
                        Genera = familyGroup
                            .GroupBy(b => b.Genus)
                            .Select(genusGroup => new GenusGroupViewModel
                            {
                                Genus = genusGroup.Key,
                                Species = genusGroup.ToList().Select(b => new SpeciesViewModel
                                {
                                    Name = b.Name,
                                    PicturesCount = b.PicturesCount,
                                    Species = b.Species,
                                    Countries = b.Countries
                                })
                            })
                    })
            });
    }
}
