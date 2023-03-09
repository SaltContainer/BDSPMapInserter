using BDSPMapInserter.Engine.MessageEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.Main.Model
{
    internal class ClonableMapInfoData
    {
        public int ZoneID { get; set; }
        public string Name { get; set; }

        public ClonableMapInfoData(int zoneID, string name)
        {
            ZoneID = zoneID;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, ZoneID);
        }
    }
}
