using MyBirds.Application.Abstract;
using MyBirds.Application.Dtos;
using MyBirds.Application.Helpers;
using MyBirds.Domain.Classifications;

namespace MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;

internal class RegisterPhotosAndTaxonomyCommandHandler(
    IOrderReadRepository orderReadRepository,
    IFamilyReadRepository familyReadRepository,
    IGenusReadRepository genusReadRepository,
    IPhotoReadRepository photoReadRepository,
    IOrderWriteRepository orderWriteRepository,
    IFamilyWriteRepository familyWriteRepository,
    IGenusWriteRepository genusWriteRepository)
    : IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand>
{
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;
    private readonly IFamilyReadRepository _familyReadRepository = familyReadRepository;
    private readonly IGenusReadRepository _genusReadRepository = genusReadRepository;
    private readonly IPhotoReadRepository _photoReadRepository = photoReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository = orderWriteRepository;
    private readonly IFamilyWriteRepository _familyWriteRepository = familyWriteRepository;
    private readonly IGenusWriteRepository _genusWriteRepository = genusWriteRepository;

    public async Task HandleAsync(RegisterPhotosAndTaxonomyCommand command, CancellationToken cancellationToken)
    {
        var birdResourcePathDtos = BirdResourcePathParser.Parse(command.PhotoPaths, command.BasePath);

        var photoNames = birdResourcePathDtos.Select(dto => dto.ResourceName);
        var photos = await _photoReadRepository.GetMissingByNamesAsync(photoNames, cancellationToken);
        var newResourcesPathDtos = birdResourcePathDtos.Where(dto => photos.Contains(dto.ResourceName));

        var distinctOrderNames = newResourcesPathDtos.Select(dto => dto.OrderName).Distinct();
        // TODO: Pass birdResourcePathDtos
        await PersistNewOrdersAsync(distinctOrderNames, cancellationToken);        
        await PersisteNewFamiliesAsync(birdResourcePathDtos, cancellationToken);
        await PersistNewGeneraAsync(birdResourcePathDtos, cancellationToken);

        // Implement SAGA pattern

        /*foreach (var photoPath in relativePaths)
        {
            var x = 0;
        }*/
        // TODO: Check if Order exists - persist
        // TODO: Check if Family exists - persist
        // TODO: Check if Genus exists - persist
        // TODO: Check if Species exists - persist
        // TODO: Get image metadata
        // TODO: Persist photos
    }

    private async Task PersistNewOrdersAsync(IEnumerable<string> orderNames, CancellationToken cancellationToken)
    {
        orderNames = await _orderReadRepository.GetMissingOrdersAsync(orderNames, cancellationToken);
        if (orderNames.Any())
        {
            var ordersToAdd = orderNames.Select(order => new Order { Name = order });
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
            //var newFamilies = distinctGenusNamesWithCorrespondingFamily.Where(f => !families.Any(existingFamily => existingFamily.Name == f.FamilyName));
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
}
