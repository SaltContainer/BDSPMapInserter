using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MessageEditor.Model
{
    internal class Word
    {
        public string Data { get; set; }
        public float Width { get; set; }

        public Word(string data, float width)
        {
            Data = data;
            Width = width;
        }
    }
}
