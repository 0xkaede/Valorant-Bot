using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class GetNameModels
    {
        public string DisplayName { get; set; }

        public string Subject { get; set; }

        public string GameName { get; set; }

        public string TagLine { get; set; }
    }
}
