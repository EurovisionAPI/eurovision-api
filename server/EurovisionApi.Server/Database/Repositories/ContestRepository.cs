using EurovisionApi.Server.Database.Models.Schemes;

namespace EurovisionApi.Server.Database.Repositories;

public class ContestRepository
{
    private int[] Years { get; set; }
    private ContestReference[] ContestReferences { get; set; }
    private Dictionary<int, ContestCache> Contests { get; set; }

    public ContestRepository(Models.Dataset.Contest[] contests, string deployUrl, bool isJunior = false)
    {
        string contestsUrl = $"{deployUrl}api/{(isJunior ? "junior" : "senior")}/contests";

        CacheContest(contests, contestsUrl);
    }

    private void CacheContest(Models.Dataset.Contest[] contests, string contestsUrl)
    {
        int[] years = new int[contests.Length];
        ContestReference[] references = new ContestReference[contests.Length];
        Dictionary<int, ContestCache> cacheContests = new Dictionary<int, ContestCache>(contests.Length);

        for (int i = 0; i < contests.Length; i++)
        {
            Models.Dataset.Contest contest = contests[i];

            years[i] = contest.Year;
            references[i] = GetContestReference(contest, contestsUrl);
            cacheContests.Add(contest.Year, new ContestCache(contest, contestsUrl));
        }

        Years = years;
        ContestReferences = references;
        Contests = cacheContests;
    }

    private ContestReference GetContestReference(Models.Dataset.Contest contest, string contestsUrl)
    {
        return new ContestReference
        {
            Year = contest.Year,
            Arena = contest.Arena,
            City = contest.City,
            Country = contest.Country,
            IntendedCountry = contest.IntendedCountry,
            Slogan = contest.Slogan,
            LogoUrl = contest.LogoUrl,
            Url = $"{contestsUrl}/{contest.Year}"
        };
    }

    public int[] GetYears()
    {
        return Years;
    }

    public ContestReference[] GetContests()
    {
        return ContestReferences;
    }

    public Contest GetContest(int year)
    {
        Contest result;

        if (Contests.TryGetValue(year, out ContestCache cache))
            result = cache.Contest;
        else
            result = null;

        return result;
    }

    public Contestant GetContestant(int year, int id)
    {
        Contestant result;

        if (Contests.TryGetValue(year, out ContestCache cache))
            result = cache.Contestants.FirstOrDefault(contestant => contestant.Id == id);
        else
            result = null;

        return result;
    }
}
