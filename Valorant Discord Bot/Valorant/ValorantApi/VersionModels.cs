using Newtonsoft.Json;
namespace Valorant_Discord_Bot.Valorant.ValorantApi
{
    public class VersionModels
    {
        [JsonProperty("data")]
        public VData data { get; set; }
    }

    public class VData
    {
        [JsonProperty("riotClientVersion")]
        public string Version { get; set; }
    }
}
