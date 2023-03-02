using BDSPMapInserter.Data;
using BDSPMapInserter.Engine.MapEditor;
using BDSPMapInserter.Engine.MessageEditor;
using BDSPMapInserter.Engine.ScriptEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine
{
    internal class MainEngine
    {
        private MapEditorEngine mapEditor;
        private MessageEditorEngine messageEditor;
        private ScriptEditorEngine scriptEditor;

        public MainEngine()
        {
            mapEditor = new MapEditorEngine();
            messageEditor = new MessageEditorEngine();
            scriptEditor = new ScriptEditorEngine();
        }

        public bool SetBasePath(string path)
        {
            return BundleManipulator.SetBasePath(path);
        }

        public string GetBasePath()
        {
            return BundleManipulator.GetBasePath();
        }
    }
}
