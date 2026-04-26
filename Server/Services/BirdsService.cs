using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MyBirds.Server.Options;
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
    private const string _birdsCacheKey = "all-birds";

    private readonly IDistributedCache _cache;
    private readonly string _path;

    public BirdsService(IDistributedCache cache, IOptions<PhotoStorageOptions> photoStorageOptions)
    {
        _cache = cache;
        _path = photoStorageOptions.Value.PhotoRootPath;
    }

    public async Task<IEnumerable<BirdSpecies>> GetAll(CancellationToken cancellationToken)
    {
        var json = await _cache.GetStringAsync(_birdsCacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(json))
            return JsonConvert.DeserializeObject<IEnumerable<BirdSpecies>>(json)!;

        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException($"Folder {_path} does not exist.");

        var birds = LoadBirds(_path);
        await _cache.SetStringAsync(_birdsCacheKey, JsonConvert.SerializeObject(birds), cancellationToken);
        return birds;

    }

    private static IEnumerable<BirdSpecies> LoadBirds(string rootPath)
    {
        var result = new List<BirdSpecies>();

        var orders = Directory.GetDirectories(rootPath);
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
                Genus = speciesFolderName.Split(' ')[0],
                Name = speciesFolderName.Split(" - ").Last(),
                Order = order.Split("\\").Last(),
                PicturesCount = picturesPath.Length,
                Species = speciesFolderName.Split(" - ")[0],
                Countries = countries.Distinct()
            };
        }
        catch (IndexOutOfRangeException)
        {
            throw new ArgumentException($"Wrong naming for {spec}", nameof(spec));
        }
    }
}
