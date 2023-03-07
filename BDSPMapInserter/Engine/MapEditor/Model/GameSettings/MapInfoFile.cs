using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class MapInfoFile
    {
        public List<ZoneData> ZoneData { get; set; }
        public List<Camera> Camera { get; set; }
        public string FileName { get; set; }
        public long PathID { get; set; }

        public MapInfoFile(List<ZoneData> zoneData, List<Camera> camera, string fileName, long pathID)
        {
            ZoneData = zoneData;
            Camera = camera;
            FileName = fileName;
            PathID = pathID;
        }
    }
}
