# Information

This is a **consumption-only** API, only the HTTP GET method is available on resources. All endpoint responses are in JSON format.

No authentication is required to access this API, and all resources are fully open and available.

# Countries

A dictionary of strings with the relationship between the codes and the names of the countries that have ever participated in the contest.

@{
    "endpoint": "countries",
    "example": {
        "AL": "Albania",
        "AD": "Andorra",
        "AM": "Armenia",
        "AU": "Australia",
        "AT": "Austria",
        "AZ": "Azerbaijan"
    }
}@

# Contests

The contests are the annual editions.

## Get all contests (endpoint)

It returns a collection of all references to contests held. Each contest reference contains the most significant data of the contest and the endpoint to obtain the contest details.

@{
    "endpoint": "senior/contests",
    "example": [{
        "year": 1956,
        "arena": "Teatro Kursaal",
        "city": "Lugano",
        "country": "CH",
        "intendedCountry": null,
        "slogan": null,
        "logoUrl": "https://raw.githubusercontent.com/EurovisionAPI/dataset/main/logos/senior/1956.png",
        "url": "https://eurovisionapi.runasp.net/api/contests/1956"
    },    
    {
        "year": 2012,
        "arena": "Crystal Hall",
        "city": "Baku",
        "country": "AZ",
        "intendedCountry": null,
        "slogan": "Light Your Fire!",
        "logoUrl": "https://raw.githubusercontent.com/EurovisionAPI/dataset/main/logos/senior/2012.png",
        "url": "https://eurovisionapi.runasp.net/api/contests/2012"
    },    
    {
        "year": 2023,
        "arena": "Liverpool Arena",
        "city": "Liverpool",
        "country": "GB",
        "intendedCountry": "UA",
        "slogan": "United By Music",
        "logoUrl": "https://raw.githubusercontent.com/EurovisionAPI/dataset/main/logos/senior/2023.png",
        "url": "https://eurovisionapi.runasp.net/api/contests/2023"
    }]
}@

### Contest reference (scheme)

| Attribute       | Type    | Description                                                                                           |  
| --------------- | ------- | ----------------------------------------------------------------------------------------------------- |
| year            | integer | Year in which the contest was held                                                                    |
| arena           | string  | Building where the contest was held                                                                   |
| city            | string  | Host city                                                                                             |
| country         | string  | Host country code                                                                                     |
| intendedCountry | string  | If not null stores the code of the country that should have been the host but couldn't (Ukraine 2023) |
| slogan          | string  | Slogan of the contest                                                                                 |
| logoUrl         | string  | Link to contest thumbnail                                                                             |
| url             | string  | Endpoint to get contest details                                                                       |


## Get contest details (endpoint)

It returns all data for a contest. The contestants in the contest is a collection of references to contestants. As with contest references, contestant references include basic contest data and an endpoint to retrieve contestant details.

@{
    "endpoint": "senior/contests/{year}",
    "example": {
        "year": 2023,
        "arena": "Liverpool Arena",
        "city": "Liverpool",
        "country": "GB",
        "intendedCountry": "UA",
        "slogan": "United By Music",
        "logoUrl": "https://raw.githubusercontent.com/EurovisionAPI/dataset/main/logos/senior/2023.png",
        "broadcasters": [
            "BBC"
        ],
        "presenters": [
            "Alesha Dixon"
        ],
        "contestants": [
            {
                "id": 0,
                "country": "SE",
                "artist": "Loreen",
                "song": "Tattoo",
                "url": "https://eurovisionapi.runasp.net/api/contests/2023/contestants/0"
            }
        ],
        "rounds": [
            {
                "name": "final",
                "date": "2023-05-13",
                "time": "19:00:00",
                "disqualifieds": null,
                "performances": [
                    {
                        "contestantId": 0,
                        "running": 9,
                        "place": 1,
                        "scores": [
                            {
                                "name": "total",
                                "points": 20,
                                "votes": {
                                    "AL": 10,
                                    "AM": 10
                                }
                            },
                            {
                                "name": "public",
                                "points": 15,
                                "votes": {
                                    "AL": 10,
                                    "AM": 5
                                }
                            },
                            {
                                "name": "jury",
                                "points": 5,
                                "votes": {
                                    "AL": 0,
                                    "AM": 5
                                }
                            }
                        ]
                    }
                ]
            },
            {
                "name": "semifinal1",
                "date": "2023-05-09",
                "time": "19:00:00",
                "disqualifieds": [
                    2, 
                    5
                ],
                "performances": [
                    {
                        "contestantId": 0,
                        "running": 11,
                        "place": 2,
                        "scores": [
                            {
                                "name": "total",
                                "points": 20,
                                "votes": {
                                    "AL": 10,
                                    "AM": 10
                                }
                            },
                            {
                                "name": "public",
                                "points": 15,
                                "votes": {
                                    "AL": 10,
                                    "AM": 5
                                }
                            },
                            {
                                "name": "jury",
                                "points": 5,
                                "votes": {
                                    "AL": 0,
                                    "AM": 5
                                }
                            }
                        ]
                    }
                ]
            }
        ]
    }
}@

### Contest (scheme)

| Attribute       | Type                   | Description                                                                                           |  
| --------------- | ---------------------- | ----------------------------------------------------------------------------------------------------- |
| year            | integer                | Year in which the contest was held                                                                    |
| arena           | string                 | Building where the contest was held                                                                   |
| city            | string                 | Host city                                                                                             |
| country         | string                 | Host country code                                                                                     |
| intendedCountry | string                 | If not null stores the code of the country that should have been the host but couldn't (Ukraine 2023) |
| slogan          | string                 | Slogan of the contest                                                                                 |
| logoUrl         | string                 | Link to contest thumbnail                                                                             |
| presenters      | string[]               | Presenters of the edition                                                                             |
| broadcasters    | string[]               | Host broadcasters of the contest                                                                      |
| contestants     | Contestant reference[] | Endpoint to get contestant details                                                                    |
| rounds          | Round[]                | All rounds of the contest                                                                             |

### Contestant reference (scheme)

| Attribute | Type    | Description                             |  
| --------- | ------- | --------------------------------------- |
| id        | integer | Contestant ID (used in Performance)     |
| country   | string  | Code of the country that is represented |
| artist    | string  | Name of the singer/group performing     |
| song      | string  | Song title                              |
| url       | string  | Endpoint to get contestant details      |

### Round (scheme)

| Attribute     | Type          | Description                                                                                                                   |
| ------------- | ------------- | ----------------------------------------------------------------------------------------------------------------------------- |
| name          | string        | Round name (final, semifinal if the year is between 2004 and 2007, semifinal1 or semifinal2 if the year is greater than 2007) |
| date          | string        | Date when the round was held in UTC                                                                                           |
| time          | string        | Time when the round was held in UTC                                                                                           |
| performances  | Performance[] | Results of the performances of the contestants in this round                                                                  |
| disqualifieds | int[]         | The id of the contestants who have been disqualified in the round                                                             |

### Performance (scheme)

| Attribute    | Type     | Description                        |
| ------------ | -------- | ---------------------------------- |
| contestantId | integer  | Contestant ID                      |
| running      | integer  | Place on the running               |
| place        | integer? | Place in the ranking (can be null) |
| scores       | Score[]  | Score and voting                   |

### Score (scheme)

| Attribute | Type                        | Description                                                              |
| --------- | --------------------------- | ------------------------------------------------------------------------ |
| name      | string                      | Origin of points (total, tele and jury if the year is greater than 2015) |
| points    | integer                     | Total points earned                                                      |
| votes     | Dictionary<string, integer> | Votes received from each country (using the country code)                |


## Get contestant details (endpoint)

It returns all about a contestant country and its song.

@{
    "endpoint": "senior/contests/{year}/contestants/{id}",
    "example": {
        "id": 0,
        "country": "SE",
        "artist": "Loreen",
        "song": "Tattoo",
        "videoUrls": [
            "https://www.youtube-nocookie.com/embed/BE2Fj0W4jP4"
        ],
        "lyrics": [
            {
            "type": 0,
            "languages": [
                "english"
            ],
            "displayedLanguages": null,
            "title": "Tattoo",
            "content": "I don't wanna go\nBut baby we both know\nThis is not our time\nIt's time to say goodbye\n\nUntil we meet again\n'Cause this is not the end\nIt will come a day\nWhen we will find our way\n\nViolins playing and the angels crying\nWhen the stars align then I'll be there\n\nNo I don't care about them all\n'Cause all I want is to be loved\nAnd all I care about is you\nYou stuck on me like a tattoo\n\nNo I don't care about the pain\nI'll walk through fire and through rain\nJust to get closer to you\nYou stuck on me like a tattoo\n\nI'm letting my hair down\nI'm taking it cool\nYou got my heart in your hand\nDon't lose it my friend\nIt's all that I got\n\nViolins playing and the angels crying\nWhen the stars align then I'll be there\n\nNo I don't care about them all\n'Cause all I want is to be loved\nAnd all I care about is you\nYou stuck on me like a tattoo\n\nNo I don't care about the pain\nI'll walk through fire and through rain\nJust to get closer to you\nYou stuck on me like a tattoo\n\nOh I don't care about them all\n'Cause all I want is to be loved\nAnd all I care about is you\nYou stuck on me like a tattoo\n\nI don't care about the pain\nI'll walk through fire and through rain\nJust to get closer to you\nYou stuck on me like a tattoo\n\nAll I care about is love\nOh oh oh\nAll I care about is love\n\nYou ѕtuck on me like a tаttoo"
            }
        ],
        "bpm": 150,
        "tone": "Eb minor",
        "artistPeople": [
            "Lorine Zineb Nora Talhaoui"
        ],
        "backings": null,
        "dancers": null,
        "stageDirector": "Sacha Jean-Baptiste",
        "composers": null,
        "conductor": null,
        "lyricists": null,
        "writers": [
            "Jimmy Jansson",
            "Jimmy Joker",
            "Loreen",
            "Moa Carlebecker",
            "Peter Boström",
            "Thomas G:son"
        ],
        "broadcaster": "SVT",
        "commentators": [
            "Edward af Sillén",
            "Måns Zelmerlöw"
        ],
        "jury": null,
        "spokesperson": "Farah Abadi"
    }
}@

### Contestant (scheme)

It represents each of the contestant songs of the edition.

| Attribute     | Type     | Description                                                                                        |  
| ------------- | -------- | -------------------------------------------------------------------------------------------------- |
| id            | integer  | Contestant ID (used in Performance)                                                                |
| country       | string   | Code of the country that is represented                                                            |
| artist        | string   | Name of the singer/group performing                                                                |
| song          | string   | Song title                                                                                         |
| videoUrls     | string[] | Links to Youtube videos showing the song                                                           |
| lyrics        | Lyrics[] | All lyrics of the song with translations (in the corresponding language).                          |
| bpm           | integer? | Beats per minute of the song (can be null)                                                         |
| tone          | string   | Key and scale of the song                                                                          |
| artistPeople  | string[] | The real name of the artist; if the artist is a group, the names correspond to the group’s members |
| backings      | string[] | Song backings                                                                                      |
| dancers       | string[] | Song dancers                                                                                       |
| stageDirector | string   | Song stage director                                                                                |
| composers     | string[] | Song composers                                                                                     |
| conductor     | string   | Song conductor (Only in the senior edition and up to and including 1998)                           |
| lyricists     | string[] | Song lyricists                                                                                     |
| writers       | string[] | Song writers                                                                                       |
| broadcaster   | string   | Candidate country broadcaster                                                                      |
| commentators  | string[] | Candidate country commentators                                                                     |
| jury          | string[] | The professional jury responsible for awarding the votes                                           |
| spokesperson  | string   | Candidate country spokesperson                                                                     |

### Lyrics (scheme)

It represents the original lyrics of the song and each of the translations of the lyrics (indicating their languages).

| Attribute          | Type     | Description                                                               |  
| ------------------ | -------- | ------------------------------------------------------------------------- |
| type               | integer  | Specifies the lyrics version (0 = Original, 1 = Translation, 2 = Version) |
| languages          | string[] | All languages that contains the song lyrics                               |
| displayedLanguages | string[] | Indicates the visual language or dialect used to render the lyrics        |
| title              | string   | The song title                                                            |
| content            | string   | The song lyrics, paragraphs are separated by double line break ("\n\n")   |

## Years (endpoint)

It returns a list of all the years in which an edition has been held.

@{
    "endpoint": "senior/contests/years",
    "example": [
        1956,
        1957,
        1958,
        1959,
        1960
    ]
}@


# Junior Contests 

The endpoints and schemas for the junior edition of the contest are the same as for the senior edition. However, for the junior edition there is less data and some fields may be returned null or empty.

You just need to replace the prefix "senior" with "junior" at all endpoints.

For example, if you want to get all the years that Junior Eurovision has been held:

@{
    "endpoint": "junior/contests/years",
    "example": [
        2003,
        2004,
        2005,
        2006,
        2007
    ]
}@
