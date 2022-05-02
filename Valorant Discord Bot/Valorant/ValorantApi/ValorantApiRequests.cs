using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.ValorantApi
{
    public class ValorantApiRequests
    {
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

        public static string GetVersion()
        {
            var webRequest = WebRequest.Create($"https://valorant-api.com/v1/version");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    VersionModels versionModels = JsonConvert.DeserializeObject<VersionModels>(response);

                    return versionModels.data.Version;
                }
            }
        }
    }
}
