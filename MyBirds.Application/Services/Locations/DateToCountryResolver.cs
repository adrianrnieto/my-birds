using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public class DateToCountryResolver : IDateToCountryResolver
{
    private static readonly DateRangeCountry[] DateRanges =
    {
        new(new DateTime(2013, 9, 14), new DateTime(2013, 9, 21, 23, 59, 59), Country.DominicanRepublic),
        new(new DateTime(2016, 5, 13), new DateTime(2016, 5, 23, 23, 59, 59), Country.Mexico),
        new(new DateTime(2017, 4, 20), new DateTime(2017, 4, 27, 23, 59, 59), Country.Germany),
        new(new DateTime(2017, 11, 11), new DateTime(2017, 11, 20, 23, 59, 59), Country.CzechRepublic),
        new(new DateTime(2018, 9, 1), new DateTime(2018, 9, 10, 23, 59, 59), Country.Germany),
        new(new DateTime(2018, 9, 11), new DateTime(2018, 9, 15, 23, 59, 59), Country.Belgium),
        new(new DateTime(2018, 10, 5), new DateTime(2018, 10, 7, 23, 59, 59), Country.France),
        new(new DateTime(2019, 5, 1), new DateTime(2019, 5, 6, 23, 59, 59), Country.France),
        new(new DateTime(2019, 6, 20), new DateTime(2019, 6, 22, 23, 59, 59), Country.Singapore),
        new(new DateTime(2019, 6, 23), new DateTime(2019, 7, 14, 23, 59, 59), Country.Indonesia),
        new(new DateTime(2019, 7, 15), new DateTime(2019, 7, 27, 23, 59, 59), Country.SriLanka),
        new(new DateTime(2021, 9, 5), new DateTime(2021, 9, 13, 23, 59, 59), Country.Kenya),
        new(new DateTime(2023, 3, 19), new DateTime(2023, 4, 11, 23, 59, 59), Country.Thailand),
        new(new DateTime(2024, 1, 5), new DateTime(2024, 1, 25, 23, 59, 59), Country.Philippines),
        new(new DateTime(2024, 1, 26), new DateTime(2024, 2, 24, 23, 59, 59), Country.Thailand),
        new(new DateTime(2024, 2, 25), new DateTime(2024, 4, 9, 23, 59, 59), Country.Vietnam),
        new(new DateTime(2024, 4, 10), new DateTime(2024, 4, 13, 23, 59, 59), Country.Netherlands),
    };

    public Country ResolveByCreationDate(DateTime creationDate)
    {
        foreach (var range in DateRanges)
        {
            if (creationDate >= range.StartDate && creationDate <= range.EndDate)
            {
                return range.Country;
            }
        }

        return Country.Spain;
    }
}
