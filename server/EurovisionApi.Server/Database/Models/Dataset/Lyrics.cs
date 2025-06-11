namespace EurovisionApi.Server.Database.Models.Dataset;

public class Lyrics
{
    public LyricsType Type { get; set; }
    public string[] Languages { get; set; }
    public string[] DisplayedLanguages { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

public enum LyricsType
{
    Original,
    Translation,
    Version
}
