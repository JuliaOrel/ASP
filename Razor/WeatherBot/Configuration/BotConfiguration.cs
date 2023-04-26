using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBot.Configuration
{
    public class BotConfiguration
    {
        public static readonly string Configuration = "BotConfiguration";
        public string BotToken { get; set; }
        public string HostAddress { get; set; }
        public string Route { get; set; }
    }
}
