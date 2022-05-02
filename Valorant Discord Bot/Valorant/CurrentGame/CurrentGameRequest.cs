using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Valorant_Discord_Bot.CommandsServices;
using Valorant_Discord_Bot.Valorant.ValorantApi;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    internal class CurrentGameRequest
    {
        public static string GetFetchPlayer(string puuid, string region)
        {
            var webRequest = WebRequest.Create($"https://glz-{region}-1.{region}.a.pvp.net/core-game/v1/players/{puuid}");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            webRequest.Headers.Add("X-Riot-Entitlements-JWT", $"{ValorantAuthCommands.EntitlementToken}");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }

        public static string GetFetchMacth(string matchID, string region)
        {
            var webRequest = WebRequest.Create($"https://glz-{region}-1.{region}.a.pvp.net/core-game/v1/matches/{matchID}");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            webRequest.Headers.Add("X-Riot-Entitlements-JWT", $"{ValorantAuthCommands.EntitlementToken}");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }

        public static string GetFetchCompetitiveUpdates(string Uuuid, string region)
        {
            var webRequest = WebRequest.Create($"https://pd.{region}.a.pvp.net/mmr/v1/players/{Uuuid}/competitiveupdates?queue=competitive&endIndex=3&startIndex=0");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            webRequest.Headers.Add("X-Riot-Entitlements-JWT", $"{ValorantAuthCommands.EntitlementToken}");
            webRequest.Headers.Add("X-Riot-ClientPlatform", $"eyJwbGF0Zm9ybVR5cGUiOiJQQyIsInBsYXRmb3JtT1MiOiJXaW5kb3dzIiwicGxhdGZvcm1PU1ZlcnNpb24iOiIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwicGxhdGZvcm1DaGlwc2V0IjoiVW5rbm93biJ9");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }

        public static string GetAgentInfo(string uuid)
        {
            var webRequest = WebRequest.Create($"https://valorant-api.com/v1/agents/{uuid}");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    return response;
                }
            }
        }

        public static string GetCompetitiveTiers()
        {
            var webRequest = WebRequest.Create($"https://valorant-api.com/v1/competitivetiers/e4e9a692-288f-63ca-7835-16fbf6234fda");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";

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
