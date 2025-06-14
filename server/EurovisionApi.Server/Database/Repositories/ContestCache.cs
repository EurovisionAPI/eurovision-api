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
            VideoUrls = contestant.VideoUrls,
            Lyrics = contestant.Lyrics,
            Bpm = contestant.Bpm,
            Tone = contestant.Tone,

            ArtistPeople = contestant.ArtistPeople,
            Backings = contestant.Backings,
            Dancers = contestant.Dancers,
            StageDirector = contestant.StageDirector,
            
            Composers = contestant.Composers,
            Conductor = contestant.Conductor,
            Lyricists = contestant.Lyricists,
            Writers = contestant.Writers,
            
            Broadcaster = contestant.Broadcaster,
            Commentators = contestant.Commentators,
            Jury = contestant.Jury,
            Spokesperson = contestant.Spokesperson
        };
    }
}
