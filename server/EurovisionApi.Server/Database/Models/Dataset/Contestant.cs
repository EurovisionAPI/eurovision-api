using EurovisionApi.Server.Database.Models.Common;

namespace EurovisionApi.Server.Database.Models.Dataset;

public class Contestant : IContestant
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string Artist { get; set; }
    public string Song { get; set; }
    public Lyrics[] Lyrics { get; set; }
    public string[] VideoUrls { get; set; }
    public string[] Dancers { get; set; }
    public string[] Backings { get; set; }
    public string[] Composers { get; set; }
    public string[] Lyricists { get; set; }
    public string[] Writers { get; set; }
    public string Conductor { get; set; }
    public string StageDirector { get; set; }
    public string Tone { get; set; }
    public int? Bpm { get; set; }
    public string Broadcaster { get; set; }
    public string Spokesperson { get; set; }
    public string[] Commentators { get; set; }
}
