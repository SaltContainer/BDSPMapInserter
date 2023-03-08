using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Data
{
    internal class CharacterInfo
    {
        [JsonProperty("char")]
        public string Character { get; set; }
        [JsonProperty("length")]
        public float Length { get; set; }
    }
}
