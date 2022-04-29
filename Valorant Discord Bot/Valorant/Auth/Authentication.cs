using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Valorant_Discord_Bot.CommandsServices;
using Valorant_Discord_Bot.DataBase;

namespace Valorant_Discord_Bot.Valorant.Auth
{
    internal class Authentication
    {
        public static CookieContainer cookiejar;
        public static bool Show2FAInput = false;

        public static bool Login(string username, string password)
        {
            cookiejar = new CookieContainer();
            GetAuthorization(cookiejar);

            var authenticate = Authenticate(cookiejar, username, password);

            var authJson = JsonConvert.DeserializeObject(authenticate);
            JToken authObject = JObject.FromObject(authJson);
            if (authenticate.Contains("auth_failure"))
            {
                return false;
            }

            var authURL = authObject["response"]["parameters"]["uri"].Value<string>();
            var access_tokenVar = Regex.Match(authURL, @"access_token=(.+?)&scope=").Groups[1].Value;
            ValorantAuthCommands.AccessToken = access_tokenVar;

            RestClient client = new RestClient(new Uri("https://entitlements.auth.riotgames.com/api/token/v1"));
            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", $"Bearer {ValorantAuthCommands.AccessToken}");
            request.AddJsonBody("{}");

            var response = client.Execute(request).Content;
            var entitlement_token = JsonConvert.DeserializeObject(response);
            JToken entitlement_tokenObj = JObject.FromObject(entitlement_token);

            ValorantAuthCommands.EntitlementToken = entitlement_tokenObj["entitlements_token"].Value<string>();

            return true;
        }

        static readonly string auth = "https://auth.riotgames.com/api/v1/authorization";

        public static void GetAuthorization(CookieContainer cookieContainer)
        {
            RestClient client = new RestClient(auth);

            client.CookieContainer = cookieContainer;

            var body = "{\"client_id\":\"play-valorant-web-prod\",\"nonce\":\"1\",\"redirect_uri\":\"https://playvalorant.com/opt_in" + "\",\"response_type\":\"token id_token\",\"scope\":\"account openid\"}";

            RestRequest request = new RestRequest(Method.POST);

            request.AddJsonBody(body);
            client.Execute(request);
        }

        public static void GetUserInfo(string AccessToken)
        {
            var webRequest = WebRequest.Create("https://auth.riotgames.com/userinfo");

            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", $"Bearer {AccessToken}");

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var response = sr.ReadToEnd();

                    UserModel UserResponse = JsonConvert.DeserializeObject<UserModel>(response);

                    ValorantAuthCommands.puuid = UserResponse.puuid;
                    ValorantAuthCommands.username = UserResponse.acct.name + "#" + UserResponse.acct.tag;
                }
            }
        }

        public static string Authenticate(CookieContainer cookie, string user, string pass)
        {
            string url = "https://auth.riotgames.com/api/v1/authorization";
            RestClient client = new RestClient(url);

            client.CookieContainer = cookie;

            RestRequest request = new RestRequest(Method.PUT);
            string body = "{\"type\":\"auth\",\"username\":\"" + user + "\",\"password\":\"" + pass + "\"}";
            request.AddJsonBody(body);

            return client.Execute(request).Content;
        }
    }
}