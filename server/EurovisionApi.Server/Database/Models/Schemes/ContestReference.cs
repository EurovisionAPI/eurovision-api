using EurovisionApi.Server.Database.Models.Common;

namespace EurovisionApi.Server.Database.Models.Schemes;

public class ContestReference : IContest, IReference
{
    public int Year { get; set; }
    public string Arena { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string IntendedCountry { get; set; }
    public string Slogan { get; set; }
    public string LogoUrl { get; set; }
    public string Url { get; set; }
}
