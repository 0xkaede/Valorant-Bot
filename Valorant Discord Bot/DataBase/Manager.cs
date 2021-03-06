using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Valorant_Discord_Bot.Utilities;
using static Valorant_Discord_Bot.DataBase.DataEmums;

namespace Valorant_Discord_Bot.DataBase
{
    internal class Manager
    {
        public static readonly string DataBaseL = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ValorantDataBase";

        public static DataBaseResponse CreateUser(DataModels model)
        {
            try
            {
                if (!Directory.Exists(DataBaseL))
                    Directory.CreateDirectory(DataBaseL);

                if (File.Exists($"{DataBaseL}\\{model.valorantDetails.Username}.Data"))
                    return DataBaseResponse.AlreadyExits;

                File.WriteAllText($"{DataBaseL}\\{model.valorantDetails.Username}.Data", JsonConvert.SerializeObject(model, Formatting.Indented));

                Logs.DataBase($"{model.valorantDetails.Username} was added to the DataBase");

                return DataBaseResponse.Successful;
            }
            catch (Exception ex)
            {
                return DataBaseResponse.Error;
            }
        }

        public static bool DeleteUser(DataModels model)
        {
            try
            {
                if (!Directory.Exists(DataBaseL))
                    Directory.CreateDirectory(DataBaseL);

                if (!File.Exists($"{DataBaseL}\\{model.valorantDetails.Username}.Data"))
                    return false;

                Logs.DataBase($"{model.valorantDetails.Username} was removed to the DataBase");

                File.Delete($"{DataBaseL}\\{model.valorantDetails.Username}.Data");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string ListEveryone()
        {
            try
            {
                string names = "";
                if (!Directory.Exists(DataBaseL))
                    Directory.CreateDirectory(DataBaseL);

                DirectoryInfo d = new DirectoryInfo(DataBaseL);
                FileInfo[] Files = d.GetFiles("*.Data");

                foreach (FileInfo file in Files)
                {
                    names += $"{Path.GetFileNameWithoutExtension(file.FullName)}\n";
                }
                return names;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static bool UpdatePassword(DataModels model, string newpassword)
        {
            try
            {
                if (!Directory.Exists(DataBaseL))
                    Directory.CreateDirectory(DataBaseL);

                if (!File.Exists($"{DataBaseL}\\{model.valorantDetails.Username}.Data"))
                    return false;

                model.valorantDetails.password = newpassword;

                File.WriteAllText($"{DataBaseL}\\{model.valorantDetails.Username}.Data", JsonConvert.SerializeObject(model, Formatting.Indented));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataBaseResponse CheckForUser(string username)
        {
            if (!File.Exists($"{DataBaseL}\\{username}.Data"))
                return DataBaseResponse.NotFound;
            else
                return DataBaseResponse.Found;
        }

        public static string UserInfo(string username)
        {
            return File.ReadAllText($"{DataBaseL}\\{username}.Data"); ;
        }
    }
}
