using BDSPMapInserter.Engine.MessageEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class MasterDatasFile
    {
        public List<bool> Data { get; set; }
        public string FileName { get; set; }
        public long PathID { get; set; }

        public MasterDatasFile(List<bool> data, string fileName, long pathID)
        {
            Data = data;
            FileName = fileName;
            PathID = pathID;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
