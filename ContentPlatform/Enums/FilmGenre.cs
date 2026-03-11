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
        Western,
        Crime,
        Mystery,
    }
}
