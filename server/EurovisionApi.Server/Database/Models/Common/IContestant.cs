namespace EurovisionApi.Server.Database.Models.Common;

public interface IContestant
{
    int Id { get; set; }
    string Country { get; set; }
    string Artist { get; set; }
    string Song { get; set; }
}
