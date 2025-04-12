using System.Text.Json;
using EurovisionApi.Server.Database.Models.Dataset;
using EurovisionApi.Server.Database.Repositories;

namespace EurovisionApi.Server.Database;

public class DataContext
{
    private const string DATASET_REPOSITORY_URL = "https://raw.githubusercontent.com/josago97/EurovisionDataset/refs/heads/main/dataset/";

    public IReadOnlyDictionary<string, string> Countries { get; private set; }
    public ContestRepository SeniorContests { get; private set; }
    public ContestRepository JuniorContests { get; private set; }

    public static async Task<DataContext> CreateAsync(ConfigurationManager configuration)
    {
        string deployUrl = configuration.GetValue<string>("DeployUrl");
        DataContext dataContext = new DataContext();
        await dataContext.DownloadDataAsync(deployUrl);

        return dataContext;
    }

    private DataContext() { }

    private Task DownloadDataAsync(string deployUrl)
    {
        HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri(DATASET_REPOSITORY_URL)
        };

        return Task.WhenAll(
            GetCountriesAsync(httpClient),
            GetSeniorContestsAsync(httpClient, deployUrl),
            GetJuniorContestsAsync(httpClient, deployUrl)
        );
    }

    private async Task GetCountriesAsync(HttpClient httpClient)
    {
        Countries = await GetAsync<Dictionary<string, string>>(httpClient, "countries");
    }

    private async Task GetSeniorContestsAsync(HttpClient httpClient, string deployUrl)
    {
        Contest[] contests = await GetAsync<Contest[]>(httpClient, "eurovision");
        SeniorContests = new ContestRepository(contests, deployUrl, isJunior: false);
    }

    private async Task GetJuniorContestsAsync(HttpClient httpClient, string deployUrl)
    {
        Contest[] contests = await GetAsync<Contest[]>(httpClient, "junior");
        JuniorContests = new ContestRepository(contests, deployUrl, isJunior: true);
    }

    private async Task<T> GetAsync<T>(HttpClient httpClient, string fileName)
    {
        string json = await httpClient.GetStringAsync(fileName + ".json");
        T result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Web);

        return result;
    }
}
