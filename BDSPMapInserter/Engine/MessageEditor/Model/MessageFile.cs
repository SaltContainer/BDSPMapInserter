using BDSPMapInserter.Engine.ScriptEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MessageEditor.Model
{
    internal class MessageFile
    {
        public List<MessageLabel> Labels { get; set; }
        public int LanguageID { get; set; }
        public int IsKanji { get; set; }
        public string FileName { get; set; }
        public long PathID { get; set; }

        public MessageFile(List<MessageLabel> labels, int languageId, int isKanji, string fileName, long pathID)
        {
            Labels = labels;
            LanguageID = languageId;
            IsKanji = isKanji;
            FileName = fileName;
            PathID = pathID;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
