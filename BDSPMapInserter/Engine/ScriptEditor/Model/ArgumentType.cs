using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.ScriptEditor.Model
{
    internal enum ArgumentType
    {
        Command = 0,
        Number = 1,
        Variable = 2,
        Flag = 3,
        SystemFlag = 4,
        String = 5
    }
}
