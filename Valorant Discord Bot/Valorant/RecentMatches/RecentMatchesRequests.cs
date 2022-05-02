using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Valorant_Discord_Bot.CommandsServices;
using Valorant_Discord_Bot.Valorant.ValorantApi;

namespace Valorant_Discord_Bot.Valorant.RecentMatches
{
    public class RecentMatchesRequests
    {
        public static string MatchHistory(string puuid, string region)
        {
            var webRequest = WebRequest.Create($"https://pd.{region}.a.pvp.net/match-history/v1/history/{puuid}");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            webRequest.Headers.Add("X-Riot-Entitlements-JWT", $"{ValorantAuthCommands.EntitlementToken}");
            webRequest.Headers.Add("X-Riot-ClientVersion", $"{ValorantApiRequests.GetVersion()}");
            webRequest.Headers.Add("X-Riot-ClientPlatform", $"{ValorantAuthCommands.ClientPlatform}");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }

        public static string MatchDetails(string matchid, string region)
        {
            var webRequest = WebRequest.Create($"https://pd.{region}.a.pvp.net/match-details/v1/matches/{matchid}");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            webRequest.Headers.Add("X-Riot-Entitlements-JWT", $"{ValorantAuthCommands.EntitlementToken}");
            webRequest.Headers.Add("X-Riot-ClientVersion", $"{ValorantApiRequests.GetVersion()}");
            webRequest.Headers.Add("X-Riot-ClientPlatform", $"{ValorantAuthCommands.ClientPlatform}");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }
    }
}
