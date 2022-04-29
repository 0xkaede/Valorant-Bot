using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Valorant_Discord_Bot.CommandsServices;
using Valorant_Discord_Bot.Valorant.Auth;

namespace Valorant_Discord_Bot.Valorant.Store
{
    internal class StoreRequest
    {
        public static string GetStoreFront(string puuid, string region)
        {
            var webRequest = WebRequest.Create($"https://pd.{region}.a.pvp.net/store/v2/storefront/{puuid}");

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

        public static string GetOffers(string region)
        {
            var webRequest = WebRequest.Create($"https://pd.{region}.a.pvp.net/store/v1/offers/");

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

        public static string GetWeaponSkinUuid(string Uuid)
        {
            var webRequest = WebRequest.Create($"https://valorant-api.com/v1/weapons/skinlevels/{Uuid}");

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
