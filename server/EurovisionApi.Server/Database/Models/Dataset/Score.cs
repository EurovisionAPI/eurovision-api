namespace EurovisionApi.Server.Database.Models.Dataset;

public class Score
{
    public string Name { get; set; }
    public int Points { get; set; }
    public Dictionary<string, int> Votes { get; set; }
}
