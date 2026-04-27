using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilmGenre
    {
        Drama,
        Comedy,
        [Display(Name = "Science Fiction")]
        ScienceFiction,
        Action,
        Music,
        TVMovie,
        War,
        Horror,
        Fantasy,
        Romance,
        Detective,
        Adventure,
        Historical,
        Thriller,
        Musical,
        Animation,
        Documentary,
        Family,
        Western,
        Crime,
        Mystery,
        History,
        Other,
    }
}
