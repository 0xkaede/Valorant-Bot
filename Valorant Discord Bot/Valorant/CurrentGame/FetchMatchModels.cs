using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{

    internal class FetchMatchModels
    {
        [JsonProperty("MatchID")]
        public string MatchID { get; set; }

        [JsonProperty("Version")]
        public long Version { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("MapID")]
        public string MapID { get; set; }

        [JsonProperty("ModeID")]
        public string ModeID { get; set; }

        [JsonProperty("ProvisioningFlow")]
        public string ProvisioningFlow { get; set; }

        [JsonProperty("GamePodID")]
        public string GamePodID { get; set; }

        [JsonProperty("AllMUCName")]
        public string AllMUCName { get; set; }

        [JsonProperty("TeamMUCName")]
        public string TeamMUCName { get; set; }

        [JsonProperty("TeamVoiceID")]
        public string TeamVoiceID { get; set; }

        [JsonProperty("IsReconnectable")]
        public bool IsReconnectable { get; set; }

        [JsonProperty("PostGameDetails")]
        public object PostGameDetails { get; set; }

        [JsonProperty("Players")]
        public List<Player> Players { get; set; }

        [JsonProperty("MatchmakingData")]
        public MatchmakingData MatchmakingData { get; set; }
    }

    public class PlayerIdentity
    {
        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("PlayerCardID")]
        public string PlayerCardID { get; set; }

        [JsonProperty("PlayerTitleID")]
        public string PlayerTitleID { get; set; }

        [JsonProperty("AccountLevel")]
        public int AccountLevel { get; set; }

        [JsonProperty("PreferredLevelBorderID")]
        public string PreferredLevelBorderID { get; set; }

        [JsonProperty("Incognito")]
        public bool Incognito { get; set; }

        [JsonProperty("HideAccountLevel")]
        public bool HideAccountLevel { get; set; }
    }

    public class SeasonalBadgeInfo
    {
        [JsonProperty("SeasonID")]
        public string SeasonID { get; set; }

        [JsonProperty("NumberOfWins")]
        public int NumberOfWins { get; set; }

        [JsonProperty("WinsByTier")]
        public object WinsByTier { get; set; }

        [JsonProperty("Rank")]
        public int Rank { get; set; }

        [JsonProperty("LeaderboardRank")]
        public int LeaderboardRank { get; set; }
    }

    public class Player
    {
        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("TeamID")]
        public string TeamID { get; set; }

        [JsonProperty("CharacterID")]
        public string CharacterID { get; set; }

        [JsonProperty("PlayerIdentity")]
        public PlayerIdentity PlayerIdentity { get; set; }

        [JsonProperty("SeasonalBadgeInfo")]
        public SeasonalBadgeInfo SeasonalBadgeInfo { get; set; }

        [JsonProperty("IsCoach")]
        public bool IsCoach { get; set; }
    }

    public class MatchmakingData
    {
        [JsonProperty("QueueID")]
        public string QueueID { get; set; }

        [JsonProperty("IsRanked")]
        public bool IsRanked { get; set; }
    }
}
