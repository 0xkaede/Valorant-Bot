using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.DataBase
{
    public class DataModels
    {
        [JsonProperty("ValorantDetails")]
        public ValorantDetails valorantDetails { get; set; }

        [JsonProperty("ValorantData")]
        public ValorantData valorantData { get; set; }

        [JsonProperty("DiscordData")]
        public DiscordData discordData { get; set; }
    }

    public class DiscordData
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("ID")]
        public ulong ID { get; set; }
    }

    public class ValorantDetails
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("Region")]
        public string region { get; set; }
    }

    public class ValorantData
    {
        [JsonProperty("puuid")]
        public string puuid { get; set; }

        [JsonProperty("userName")]
        public string username { get; set; }

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("EntitlementToken")]
        public string EntitlementToken { get; set; }
    }
}
