using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.Main.Model
{
    internal class InputData
    {
        public int AreaID { get; set; }
        public string ZoneCode { get; set; }
        public string AreaCode { get; set; }
        public int MapInfoCloneZoneID { get; set; }
        public bool IsSinnoh { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public string AreaName { get; set; }
        public string AreaNameDisplay { get; set; }
        public string AreaNameIndirect { get; set; }
        public string AreaNameTownMap { get; set; }
    }
}
