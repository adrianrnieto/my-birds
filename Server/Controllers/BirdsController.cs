using Microsoft.AspNetCore.Mvc;
using MyBirds.Server.Models;
using MyBirds.Server.Services;
using MyBirds.Shared;

namespace MyBirds.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class BirdsController : ControllerBase
{
    private readonly IBirdsService _birdsService;

    public BirdsController(IBirdsService birdsService)
    {
        _birdsService = birdsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var birds = await _birdsService.GetAll(CancellationToken.None);

        var viewModel = GetViewModel(birds);

        return Ok(viewModel);
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
