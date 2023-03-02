using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MessageEditor.Model
{
    internal class MessageLabel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public List<Word> Words { get; set; }

        public MessageLabel(int index, string name, List<Word> words)
        {
            Index = index;
            Name = name;
            Words = words;
        }
    }
}
