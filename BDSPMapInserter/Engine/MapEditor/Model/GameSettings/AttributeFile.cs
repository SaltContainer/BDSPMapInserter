using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class AttributeFile
    {
        public List<long> Attributes { get; set; }
        public int Width { get; set; }
        public string FileName { get; set; }
        public long PathID { get; set; }

        public AttributeFile(List<long> attributes, int width, string fileName, long pathID)
        {
            Attributes = attributes;
            Width = width;
            FileName = fileName;
            PathID = pathID;
        }
    }
}
