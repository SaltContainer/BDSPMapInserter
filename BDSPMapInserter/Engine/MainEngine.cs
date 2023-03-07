using BDSPMapInserter.Data;
using BDSPMapInserter.Engine.MapEditor;
using BDSPMapInserter.Engine.MapEditor.Model;
using BDSPMapInserter.Engine.MessageEditor;
using BDSPMapInserter.Engine.MessageEditor.Model;
using BDSPMapInserter.Engine.ScriptEditor;
using BDSPMapInserter.Engine.ScriptEditor.Model;
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

        public List<MasterDatasFile> GetMasterDatasFiles()
        {
            return mapEditor.GetMasterDatasFiles();
        }

        public MapInfoFile GetMapInfoFile()
        {
            return mapEditor.GetMapInfoFile();
        }

        public List<AttributeFile> GetAttributeFiles()
        {
            return mapEditor.GetAttributeFiles();
        }

        public Dictionary<string, List<MessageFile>> GetMessageFiles()
        {
            return messageEditor.GetMessageFiles();
        }

        public List<ScriptFile> GetScriptFiles()
        {
            return scriptEditor.GetScriptFiles();
        }

        public void SetMasterDatasFiles(List<MasterDatasFile> masterDatasFiles)
        {
            mapEditor.SetMasterDatasFiles(masterDatasFiles);
        }

        public void SetMapInfoFile(MapInfoFile mapInfoFile)
        {
            mapEditor.SetMapInfoFile(mapInfoFile);
        }

        public void SetAttributeFiles(List<AttributeFile> attributeFiles)
        {
            mapEditor.SetAttributeFiles(attributeFiles);
        }

        public void SetMessageFiles(Dictionary<string, List<MessageFile>> messageFiles)
        {
            messageEditor.SetMessageFiles(messageFiles);
        }

        public void SetScriptFiles(List<ScriptFile> scriptFiles)
        {
            scriptEditor.SetScriptFiles(scriptFiles);
        }
    }
}
