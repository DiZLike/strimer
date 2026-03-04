using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostPlayer.Models
{
    public class AppConfig
    {
        [JsonProperty]
        public int Volume { get; set; } = 1;
    }
}
