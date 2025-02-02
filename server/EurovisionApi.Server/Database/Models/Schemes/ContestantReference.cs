using EurovisionApi.Server.Database.Models.Common;

namespace EurovisionApi.Server.Database.Models.Schemes;

public class ContestantReference : IContestant, IReference
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string Artist { get; set; }
    public string Song { get; set; }
    public string Url { get; set; }
}
