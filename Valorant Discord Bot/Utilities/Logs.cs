using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Utilities
{
    static class Logs
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static bool Defualt(string a)
        {
            Console.Write(a);
            Console.ResetColor();
            return true;
        }

        public static bool Info(string a)
        {
            Defualt("[");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Defualt("INFO!");
            Defualt("] ");
            Console.WriteLine(a);
            return true;
        }

        public static bool CommandSent(string a)
        {
            Defualt("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Defualt("CMD!");
            Defualt("] ");
            Console.WriteLine(a);
            return true;
        }

        public static bool ReqAPI(string a)
        {
            Defualt("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Defualt("Req!");
            Defualt("] ");
            Console.WriteLine(a);
            return true;
        }

        public static bool Error(string a)
        {
            Defualt("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Defualt("ERROR");
            Defualt("] ");
            Console.WriteLine(a);
            return true;
        }

        public static bool Guilds(string a)
        {
            Defualt("[");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Defualt("Guild!");
            Defualt("] ");
            Console.WriteLine(a);
            return true;
        }
    }
}
