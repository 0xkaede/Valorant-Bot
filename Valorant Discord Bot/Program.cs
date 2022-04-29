using System;
using System.Threading.Tasks;

namespace Valorant_Discord_Bot
{
    class Program
    {
        public static async Task Main(string[] args)
            => await Startup.RunAsync(args);
    }
}
