using MyBirds.Application.Abstract;
using MyBirds.Application.Dtos;
using MyBirds.Application.Helpers;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;

namespace MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;

internal class RegisterPhotosAndTaxonomyCommandHandler(
    IOrderReadRepository orderReadRepository,
    IFamilyReadRepository familyReadRepository,
    IGenusReadRepository genusReadRepository,
    ISpeciesReadRepository speciesReadRepository,
    IPhotoReadRepository photoReadRepository,
    IOrderWriteRepository orderWriteRepository,
    IFamilyWriteRepository familyWriteRepository,
    IGenusWriteRepository genusWriteRepository,
    ISpeciesWriteRepository speciesWriteRepository,
    IPhotoWriteRepository photoWriteRepository)
    : IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand>
{
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;
    private readonly IFamilyReadRepository _familyReadRepository = familyReadRepository;
    private readonly IGenusReadRepository _genusReadRepository = genusReadRepository;
    private readonly ISpeciesReadRepository _speciesReadRepository = speciesReadRepository;
    private readonly IPhotoReadRepository _photoReadRepository = photoReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository = orderWriteRepository;
    private readonly IFamilyWriteRepository _familyWriteRepository = familyWriteRepository;
    private readonly IGenusWriteRepository _genusWriteRepository = genusWriteRepository;
    private readonly ISpeciesWriteRepository _speciesWriteRepository = speciesWriteRepository;
    private readonly IPhotoWriteRepository _photoWriteRepository = photoWriteRepository;

    public async Task HandleAsync(RegisterPhotosAndTaxonomyCommand command, CancellationToken cancellationToken)
    {
        var birdResourcePathDtos = BirdResourcePathParser.Parse(command.PhotoPaths, command.BasePath);

        var resourceNames = birdResourcePathDtos.Select(dto => dto.ResourceName);
        var resources = await _photoReadRepository.GetMissingByNamesAsync(resourceNames, cancellationToken);
        var newResourcesPathDtos = birdResourcePathDtos.Where(dto => resources.Contains(dto.ResourceName));
        if (!newResourcesPathDtos.Any())
        {
            return;
        }

        await PersistNewOrdersAsync(newResourcesPathDtos, cancellationToken);
        await PersisteNewFamiliesAsync(newResourcesPathDtos, cancellationToken);
        await PersistNewGeneraAsync(newResourcesPathDtos, cancellationToken);
        await PersistNewSpeciesAsync(newResourcesPathDtos, cancellationToken);
        await PersistNewPhotosAsync(newResourcesPathDtos, cancellationToken);

        // TODO: Implement as SAGA pattern
        // TODO: Send push notification to UI
    }

    private async Task PersistNewOrdersAsync(IEnumerable<BirdResourcePathDto> birdResourcePathDtos, CancellationToken cancellationToken)
    {
        var distinctOrderNames = birdResourcePathDtos.Select(dto => dto.OrderName).Distinct();
        var missingOrderNames = await _orderReadRepository.GetMissingOrdersAsync(distinctOrderNames, cancellationToken);
        if (missingOrderNames.Any())
        {
            var ordersToAdd = missingOrderNames.Select(orderName => new Order { Name = orderName });
            await _orderWriteRepository.AddAsync(ordersToAdd, cancellationToken);
        }
    }

    private async Task PersisteNewFamiliesAsync(IEnumerable<BirdResourcePathDto> birdResourcePathDtos, CancellationToken cancellationToken)
    {
        var distinctFamilyNamesWithCorrespondingOrder = birdResourcePathDtos.Select(dto => (dto.OrderName, dto.FamilyName)).Distinct();
        var distinctFamilyNames = distinctFamilyNamesWithCorrespondingOrder.Select(f => f.FamilyName);
        var missingFamilies = await _familyReadRepository.GetMissingFamiliesAsync(distinctFamilyNames, cancellationToken);
        if (missingFamilies.Any())
        {
            var missingFamilyNamesWithCorrespondingOrder = distinctFamilyNamesWithCorrespondingOrder.Where(f => missingFamilies.Contains(f.FamilyName));
            var distinctOrders = missingFamilyNamesWithCorrespondingOrder.Select(f => f.OrderName).Distinct();
            var orders = await _orderReadRepository.GetByNamesAsync(distinctOrders, cancellationToken);
            var familiesToAdd = missingFamilyNamesWithCorrespondingOrder.Select(familyNameWithOrder =>
                new Family
                {
                    Name = familyNameWithOrder.FamilyName,
                    OrderId = orders.Single(o => o.Name == familyNameWithOrder.OrderName).Id
                }
            );
            await _familyWriteRepository.AddAsync(familiesToAdd, cancellationToken);
        }
    }

    private async Task PersistNewGeneraAsync(IEnumerable<BirdResourcePathDto> birdResourcePathDtos, CancellationToken cancellationToken)
    {
        var distinctGenusNamesWithCorrespondingFamily = birdResourcePathDtos.Select(dto => (dto.FamilyName, dto.GenusName)).Distinct();
        var distinctGenusNames = distinctGenusNamesWithCorrespondingFamily.Select(g => g.GenusName);
        var missingGenera = await _genusReadRepository.GetMissingGeneraAsync(distinctGenusNames, cancellationToken);
        if (missingGenera.Any())
        {
            var missingGenusNamesWithCorrespondingFamily = distinctGenusNamesWithCorrespondingFamily.Where(f => missingGenera.Contains(f.GenusName));
            var distinctFamilyNames = missingGenusNamesWithCorrespondingFamily.Select(dto => dto.FamilyName).Distinct();
            var families = await _familyReadRepository.GetByNamesAsync(distinctFamilyNames, cancellationToken);
            var genusesToAdd = missingGenusNamesWithCorrespondingFamily.Select(genusNameWithFamily =>
                new Genus
                {
                    Name = genusNameWithFamily.GenusName,
                    FamilyId = families.Single(f => f.Name == genusNameWithFamily.FamilyName).Id
                }
            );
            await _genusWriteRepository.AddAsync(genusesToAdd, cancellationToken);
        }
    }

    private async Task PersistNewSpeciesAsync(IEnumerable<BirdResourcePathDto> birdResourcePathDtos, CancellationToken cancellationToken)
    {
        var distinctSpeciesNamesWithCorrespondingGenus = birdResourcePathDtos.Select(dto => (dto.GenusName, dto.SpeciesName, dto.SpeciesScientificName)).Distinct();
        var distinctSpeciesNames = distinctSpeciesNamesWithCorrespondingGenus.Select(g => g.SpeciesName);
        var missingSpecies = await _speciesReadRepository.GetMissingAsync(distinctSpeciesNames, cancellationToken);
        if (missingSpecies.Any())
        {
            var missingSpeciesNamesWithCorrespondingGenus = distinctSpeciesNamesWithCorrespondingGenus.Where(f => missingSpecies.Contains(f.SpeciesName));
            var distinctGenusNames = missingSpeciesNamesWithCorrespondingGenus.Select(dto => dto.GenusName).Distinct();
            var genera = await _genusReadRepository.GetByNamesAsync(distinctGenusNames, cancellationToken);
            var speciesToAdd = missingSpeciesNamesWithCorrespondingGenus.Select(speciesNameWithGenus =>
                new Species
                {
                    Name = speciesNameWithGenus.SpeciesName,
                    ScientificName = speciesNameWithGenus.SpeciesScientificName,
                    GenusId = genera.Single(f => f.Name == speciesNameWithGenus.GenusName).Id
                }
            );
            await _speciesWriteRepository.AddAsync(speciesToAdd, cancellationToken);
        }
    }

    // TODO: Use a different hosted service to react to the new photo added to update the country information
    private async Task PersistNewPhotosAsync(IEnumerable<BirdResourcePathDto> birdResourcePathDtos, CancellationToken cancellationToken)
    {
        var distinctSpeciesNames = birdResourcePathDtos.Select(dto => dto.SpeciesName).Distinct();
        var species = await _speciesReadRepository.GetByNamesAsync(distinctSpeciesNames, cancellationToken);

        var photos = birdResourcePathDtos.Select(dto =>
            new Photo
            {
                FullPath = $"{dto.OrderName}/{dto.FamilyName}/{dto.SpeciesScientificName} - {dto.SpeciesName}/{dto.ResourceName}",
                Name = dto.ResourceName,
                SpeciesId = species.Single(s => s.Name == dto.SpeciesName).Id
            });
        await _photoWriteRepository.AddAsync(photos, cancellationToken);
    }
}
