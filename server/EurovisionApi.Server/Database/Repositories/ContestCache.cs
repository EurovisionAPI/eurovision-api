using EurovisionApi.Server.Database.Models.Schemes;

namespace EurovisionApi.Server.Database.Repositories;

public class ContestCache
{
    public Contest Contest { get; private set; }
    public Contestant[] Contestants { get; private set; }

    public ContestCache(Models.Dataset.Contest contest, string contestsUrl)
    {
        string contestUrl = $"{contestsUrl}/{contest.Year}";

        SaveContest(contest);
        SaveContestants(contest.Contestants, contestUrl);
    }

    private void SaveContest(Models.Dataset.Contest contest)
    {
        Contest = new Contest
        {
            Year = contest.Year,
            Arena = contest.Arena,
            City = contest.City,
            Country = contest.Country,
            IntendedCountry = contest.IntendedCountry,
            Slogan = contest.Slogan,
            LogoUrl = contest.LogoUrl,
            Voting = contest.Voting,
            Presenters = contest.Presenters,
            Broadcasters = contest.Broadcasters,
            Rounds = contest.Rounds
        };
    }

    private void SaveContestants(Models.Dataset.Contestant[] contestants, string contestUrl)
    {
        string contestantsUrl = $"{contestUrl}/contestants";
        List<ContestantReference> references = new List<ContestantReference>();
        List<Contestant> caches = new List<Contestant>();

        foreach (Models.Dataset.Contestant contestant in contestants)
        {
            references.Add(new ContestantReference
            {
                Id = contestant.Id,
                Country = contestant.Country,
                Artist = contestant.Artist,
                Song = contestant.Song,
                Url = $"{contestantsUrl}/{contestant.Id}"
            });

            caches.Add(SaveContestant(contestant));
        }

        Contestants = caches.ToArray();
        Contest.Contestants = references.ToArray();
    }

    private Contestant SaveContestant(Models.Dataset.Contestant contestant)
    {
        return new Contestant
        {
            Id = contestant.Id,
            Country = contestant.Country,
            Artist = contestant.Artist,
            Song = contestant.Song,
            Lyrics = contestant.Lyrics,
            VideoUrls = contestant.VideoUrls,
            Dancers = contestant.Dancers,
            Backings = contestant.Backings,
            Composers = contestant.Composers,
            Lyricists = contestant.Lyricists,
            Writers = contestant.Writers,
            Conductor = contestant.Conductor,
            StageDirector = contestant.StageDirector,
            Tone = contestant.Tone,
            Bpm = contestant.Bpm,
            Broadcaster = contestant.Broadcaster,
            Spokesperson = contestant.Spokesperson,
            Commentators = contestant.Commentators
        };
    }
}
