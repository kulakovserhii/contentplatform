using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCreditss
    {
        [JsonPropertyName("cast")]
        public List<TmdbCast> Cast { get; set; }
        [JsonPropertyName("crew")]
        public List<TmdbCrew> Crew { get; set; }
    }
}
