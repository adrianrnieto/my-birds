using MyBirds.Application.Dtos;

namespace MyBirds.Application.Services.Paths;

internal interface IBirdResourcePathParser
{
    IEnumerable<BirdResourcePathDto> Parse(IEnumerable<string> absolutePaths, string basePath);
}

internal class BirdResourcePathParser : IBirdResourcePathParser
{
    public IEnumerable<BirdResourcePathDto> Parse(IEnumerable<string> absolutePaths, string basePath)
    {
        var relativePaths = absolutePaths.Select(path => path.Replace(basePath + Path.DirectorySeparatorChar, string.Empty));
        var splitRelativePaths = relativePaths.Select(relativePath => relativePath.Split(Path.DirectorySeparatorChar));

        // TODO: Persist invalid images
        // TODO: Improve filter
        splitRelativePaths = splitRelativePaths.Where(splitPath => splitPath.Length == 4 && splitPath[2].Contains(" - "));

        return splitRelativePaths.Select(Parse);
    }

    private static BirdResourcePathDto Parse(string[] splitPath)
    {
        return new BirdResourcePathDto
        {
            OrderName = splitPath[0],
            FamilyName = splitPath[1],
            GenusName = splitPath[2].Split(' ')[0],
            SpeciesName = splitPath[2].Split(" - ")[1],
            SpeciesScientificName = splitPath[2].Split(" - ")[0],
            ResourceName = splitPath[3]
        };
    }
}
