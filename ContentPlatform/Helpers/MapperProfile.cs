using AutoMapper;
using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using ContentPlatform.Enums;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Content, ContentDetailsDto>()
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src
            => src.Reviews))
            .ForMember(dest => dest.AdditionalDetails, opt => opt.
            MapFrom(src => GetAdditionalDetails(src)));
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.RateReviews
                .Count(rr => rr.VoteType == Evaluate.Like)))
            .ForMember(dest => dest.DislikeCount, opt => opt.MapFrom(src => src.RateReviews
                .Count(rr => rr.VoteType == Evaluate.Dislike)));
        
        CreateMap<User, UserDto>();
    }
    private object? GetAdditionalDetails(Content src)
    {
        if(src is Film film)
        {
            return new
            {
                Director = film.Director,
                Writers = film.Writers,
                MainCast = film.MainCast,
                Budget = film.Budget,
                BoxOffice = film.BoxOffice,
                RuntimeInMinutes = film.RuntimeInMinutes,
                CountryOfOrigin = film.CountryOfOrigin,
                Awards = film.Awards,
                Genres = film.Genres.Select(g => g.ToString()).ToList()
            };
        }
        if(src is TVShow tvShow)
        {
            return new
            {
                Creators = tvShow.Creators,
                Director = tvShow.Director,
                MainCast = tvShow.MainCast,
                TotalSeasons = tvShow.TotalSeasons,
                TotalEpisodes = tvShow.Episodes,
                Networks = tvShow.Networks,
                EndDate = tvShow.EndDate,
                Genres = tvShow.Genres.Select(g => g.ToString()).ToList()
            };
        }
        if(src is Episode episode)
        {
            return new
            {
                TVShowId = episode.TVShowId,
                SeasonNumber = episode.SeasonNumber,
                EpisodeNumber = episode.EpisodeNumber,
                TotalNumber = episode.TotalNumber,
                RuntimeInMinutes = episode.RuntimeInMinutes
            };
        }
        if(src is Music music)
        {
            return new
            {
                Artist = music.Artist,
                Album = music.Album,
                DurationInSeconds = music.DurationInSeconds,
                Label = music.Label,
                Lanquage = music.Lanquage,
                Genres = music.Genres.Select(g => g.ToString()).ToList()
            };
        }
        if(src is Game game)
        {
            return new 
            {
                Developer = game.Developer,
                Publisher = game.Publisher,
                Platforms = game.Platforms.Select(p => p.ToString()).ToList(),
                Genres = game.Genres.Select(g => g.ToString()).ToList()
            };
        }
        if(src is Book book) 
        {
            return new
            {
                Author = book.Author,
                Publisher = book.Publisher,
                OriginalLanguage = book.OriginalLanguage,
                Pages = book.Pages,
                Genres = book.Genres.Select(g => g.ToString()).ToList()
            };
        }
        return null;
    }
}