using System.Text.Json;
using EurovisionApi.Server.Database.Models.Dataset;
using EurovisionApi.Server.Database.Repositories;
using Microsoft.Extensions.Options;

namespace EurovisionApi.Server.Database;

public class DataContext
{
    private readonly IOptions<Settings> _options;

    public IReadOnlyDictionary<string, string> Countries { get; private set; }
    public ContestRepository SeniorContests { get; private set; }
    public ContestRepository JuniorContests { get; private set; }
    private Settings Settings => _options.Value;

    public DataContext(IOptions<Settings> options)
    {
        _options = options;
    }

    public async Task DownloadDatasetAsync(string releaseName = null)
    {
        string repository = $"{Settings.DatasetRepository.Author}/{Settings.DatasetRepository.Name}";
        releaseName ??= await GetLastestReleaseNameAsync(repository);

        string deployUrl = Settings.DeployUrl;
        string downloadBaseUrl = $"https://github.com/{repository}/releases/download/{releaseName}/";
        using HttpClient httpClient = CreateHttpClient(downloadBaseUrl);

        await Task.WhenAll(
            GetCountriesAsync(httpClient),
            GetSeniorContestsAsync(httpClient),
            GetJuniorContestsAsync(httpClient)
        );
    }

    private async Task<string> GetLastestReleaseNameAsync(string repository)
    {
        using HttpClient httpClient = CreateHttpClient();
        string url = $"https://api.github.com/repos/{repository}/releases/latest";
        string response = await httpClient.GetStringAsync(url);

        using JsonDocument jsonDocument = JsonDocument.Parse(response);
        string releaseName = jsonDocument.RootElement.GetProperty("name").GetString();

        return releaseName;
    }

    private HttpClient CreateHttpClient(string baseAddress = null)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

        if (!string.IsNullOrWhiteSpace(baseAddress))
            httpClient.BaseAddress = new Uri(baseAddress);

        return httpClient;
    }

    private async Task GetCountriesAsync(HttpClient httpClient)
    {
        Countries = await GetAsync<Dictionary<string, string>>(httpClient, "countries");
    }

    private async Task GetSeniorContestsAsync(HttpClient httpClient)
    {
        Contest[] contests = await GetAsync<Contest[]>(httpClient, "senior");
        SeniorContests = new ContestRepository(contests, Settings.DeployUrl, isJunior: false);
    }

    private async Task GetJuniorContestsAsync(HttpClient httpClient)
    {
        Contest[] contests = await GetAsync<Contest[]>(httpClient, "junior");
        JuniorContests = new ContestRepository(contests, Settings.DeployUrl, isJunior: true);
    }

    private async Task<T> GetAsync<T>(HttpClient httpClient, string fileName)
    {
        string json = await httpClient.GetStringAsync(fileName + ".json");
        T result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Web);

        return result;
    }
}
