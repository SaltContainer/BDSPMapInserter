using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Data
{
    internal class ZoneInfo
    {
        [JsonProperty("ID")]
        public int Key { get; set; }
        [JsonProperty("AreaName")]
        public string AreaName { get; set; }
        [JsonProperty("FriendlyName")]
        public string FriendlyName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
