using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.ExternalApi.LastFmModels;
using ContentPlatform.Helpers;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class LastFmService(HttpClient httpClient, IConfiguration configuration) : ILastFmService
    {
        private readonly string _apiKey = configuration["LastFm:ApiKey"];
        public async Task<List<MusicCreateDto>> GetPopularMusic(int count)
        {
            var url = $"https://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key={_apiKey}&format=json&limit=75";
            var response = await httpClient.GetFromJsonAsync<LastFmTopTracksResponse>(url);
            var musicList = new List<MusicCreateDto>();
            foreach(var trackShort in response?.Tracks.TrackList ?? [])
            {
                var detailsUrl = $"https://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key={_apiKey}&artist={Uri.EscapeDataString(trackShort.Artist.Name)}&track={Uri.EscapeDataString(trackShort.Name)}&format=json";
                var details = await httpClient.GetFromJsonAsync<LastFmTrackDetailsResponse>(detailsUrl);
                if (details?.Track != null)
                    musicList.Add(MapToDto(details.Track));
            }
            return musicList;
        }

        private MusicCreateDto MapToDto(LastFmTrackDetails lftd)
        {
            string descriprion = lftd.Wiki?.Summary;
            if (string.IsNullOrWhiteSpace(descriprion))
                descriprion = $"Трек '{lftd.Name}' від виконавця {lftd.Artist.Name}.";
            return new MusicCreateDto
            {
                Title = lftd.Name,
                Description = lftd.Wiki?.Summary ?? descriprion,
                MusicArtist = lftd.Artist.Name,
                MusicAlbum = lftd.Album?.Title ?? "Single",
                MusicDurationInSeconds = int.Parse(lftd.Duration) / 1000,
                ReleaseYear = DateTime.Now.Year,
                MusicLanquage = "English",
                MusicLabel = "Independent",
                MusciGenres = MapMusicGenres(lftd.Toptags.Tags),
                ExternalId = $"{lftd.Artist.Name}-{lftd.Name}"
            };
        }

        private List<MusicGenre> MapMusicGenres(List<LastFmTag> tags)
        {
            if (tags == null || !tags.Any())
                return new List<MusicGenre> { MusicGenre.Other };
            var result = new List<MusicGenre>();
            foreach(var tag in tags)
            {
                if (MusicGenreMap.Map.TryGetValue(tag.Name, out var genre))
                    result.Add(genre);
            }
            var uniqueGenres = result.Distinct().ToList();
            return uniqueGenres.Any() ? uniqueGenres : new List<MusicGenre> { MusicGenre.Other };
        }
    }
}
