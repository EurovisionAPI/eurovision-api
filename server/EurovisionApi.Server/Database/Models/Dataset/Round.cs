namespace EurovisionApi.Server.Database.Models.Dataset;

public class Round
{
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? Time { get; set; }
    public Performance[] Performances { get; set; }
    public int[] Disqualifieds { get; set; }
}
