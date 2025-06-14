namespace EurovisionApi.Server;

public class Settings
{
    public string DeployUrl { get; init; }
    public string UpdateDatasetSecret { get; init; }
    public Repository DatasetRepository { get; init; }

    public class Repository
    {
        public string Author { get; set; }
        public string Name { get; set; }
    }
}


