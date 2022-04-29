using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.Store
{
    internal class WeaponsSkinUuuidModels
    {
        public class data
        {
            [JsonProperty("data")]
            public Information information { get; set; }
        }

        public class Information
        {
            [JsonProperty("uuid")]
            public string uuid { get; set; }

            [JsonProperty("displayName")]
            public string displayName { get; set; }

            [JsonProperty("levelItem")]
            public string levelItem { get; set; }

            [JsonProperty("displayIcon")]
            public string displayIcon { get; set; }

            [JsonProperty("streamedVideo")]
            public string streamedVideo { get; set; }

            [JsonProperty("assetPath")]
            public string assetPath { get; set; }
        }
    }
}
