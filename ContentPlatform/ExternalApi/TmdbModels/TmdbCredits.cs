using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCredits
    {
        [JsonPropertyName("cast")]
        public List<TmdbCastMember> Cast { get; set; } = new();
        [JsonPropertyName("crew")]
        public List<TmdbCrewMember> Crew { get; set; } = new();
    }
}
