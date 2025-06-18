using Microsoft.Extensions.Caching.Distributed;
using MyBirds.Server.Models;
using MyBirds.Server.Providers;
using Newtonsoft.Json;
using Directory = System.IO.Directory;

namespace MyBirds.Server.Services;

public interface IBirdsService
{
    Task<IEnumerable<BirdSpecies>> GetAll(CancellationToken cancellationToken);
}

public class BirdsService : IBirdsService
{
    private const string _path = @"C:\Users\Adrian\Pictures\FX82\Birds";
    private const string _birdsCacheKey = "all-birds";

    private readonly IDistributedCache _cache;

    public BirdsService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<IEnumerable<BirdSpecies>> GetAll(CancellationToken cancellationToken)
    {
        var json = await _cache.GetStringAsync(_birdsCacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(json))
            return JsonConvert.DeserializeObject<IEnumerable<BirdSpecies>>(json)!;

        if (!Directory.Exists(_path))
            throw new ApplicationException("El directorio no existe.");

        var birds = LoadBirds();
        await _cache.SetStringAsync(_birdsCacheKey, JsonConvert.SerializeObject(birds), cancellationToken);
        return birds;

    }

    private static IEnumerable<BirdSpecies> LoadBirds()
    {
        var result = new List<BirdSpecies>();

        var orders = Directory.GetDirectories(_path);
        foreach (var order in orders)
        {
            var families = Directory.GetDirectories(order);
            foreach (var family in families)
            {
                var species = Directory.GetDirectories(family);
                foreach (var spec in species)
                {
                    result.Add(GetViewModel(order, family, spec));
                }
            }
        }

        return result;
    }

    private static BirdSpecies GetViewModel(string order, string family, string spec)
    {
        try
        {
            var picturesPath = Directory.GetFiles(spec);
            var speciesFolderName = spec.Split("\\").Last();

            var countries = picturesPath.Select(CountryProvider.GetCountryFromFile);

            return new BirdSpecies
            {
                Family = family.Split("\\").Last(),
                Genus = speciesFolderName.Split(' ').First(),
                Name = speciesFolderName.Split(" - ").Last(),
                Order = order.Split("\\").Last(),
                PicturesCount = picturesPath.Length,
                Species = speciesFolderName.Split(" - ").First(),
                Countries = countries.Distinct()
            };
        }
        catch (IndexOutOfRangeException)
        {
            throw new ApplicationException($"Wrong naming for {spec}");
        }
    }
}
