using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookGenre
    {
        Romance,
        Fantasy,
        [Display(Name = "Science Fiction")]
        ScienceFiction,
        Detective,
        Biography,
        Historical,
        Thriller,
        Horror,
        Adventure,
        [Display(Name = "Global CLassic")]
        GloBalClassic,
        Other,
    }
}
