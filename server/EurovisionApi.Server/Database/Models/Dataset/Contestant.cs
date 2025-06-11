using EurovisionApi.Server.Database.Models.Common;

namespace EurovisionApi.Server.Database.Models.Dataset;

public class Contestant : IContestant
{
    #region General Information

    public int Id { get; set; }
    public string Country { get; set; }
    public string Artist { get; set; }
    public string Song { get; set; }
    public string[] VideoUrls { get; set; }
    public Lyrics[] Lyrics { get; set; }
    public int? Bpm { get; set; }
    public string Tone { get; set; }

    #endregion

    #region On-Stage Representation

    public string[] ArtistPeople { get; set; }
    public string[] Backings { get; set; }
    public string[] Dancers { get; set; }
    public string StageDirector { get; set; }

    #endregion

    #region Musical Team

    public string[] Composers { get; set; }
    public string Conductor { get; set; }
    public string[] Lyricists { get; set; }
    public string[] Writers { get; set; }

    #endregion

    #region Broadcasting

    public string Broadcaster { get; set; }
    public string[] Commentators { get; set; }
    public string[] Jury { get; set; }
    public string Spokesperson { get; set; }

    #endregion
}
