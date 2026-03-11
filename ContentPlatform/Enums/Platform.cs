using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Platform
    {
        PC,
        [Display(Name = "PlayStation 5")]
        PS5,
        [Display(Name = "PlayStation 4")]
        PS4,
        XBox,
        [Display(Name = "Nintendo Switch")]
        NintendoSwitch,
        Mobile,
        CrossPlatform
    }
}
