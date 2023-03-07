using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MessageEditor.Model
{
    internal class MessageLabel
    {
        public int LabelIndex { get; set; }
        public int ArrayIndex { get; set; }
        public string Name { get; set; }
        public List<Word> Words { get; set; }

        public MessageLabel(int labelIndex, int arrayIndex, string name, List<Word> words)
        {
            LabelIndex = labelIndex;
            ArrayIndex = arrayIndex;
            Name = name;
            Words = words;
        }
    }
}
