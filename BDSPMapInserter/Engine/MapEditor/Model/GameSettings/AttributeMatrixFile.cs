using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class AttributeMatrixFile
    {
        public List<long> AttributePathIDs { get; set; }
        public int Width { get; set; }
        public string FileName { get; set; }
        public long PathID { get; set; }

        public AttributeMatrixFile(List<long> attributePathIDs, int width, string fileName, long pathID)
        {
            AttributePathIDs = attributePathIDs;
            Width = width;
            FileName = fileName;
            PathID = pathID;
        }
    }
}
