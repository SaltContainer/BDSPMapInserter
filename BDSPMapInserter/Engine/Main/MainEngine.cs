using BDSPMapInserter.Data;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Engine.Main.Model;
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

namespace BDSPMapInserter.Engine.Main
{
    internal class MainEngine
    {
        private InputValidator inputValidator;

        private MapEditorEngine mapEditor;
        private MessageEditorEngine messageEditor;
        private ScriptEditorEngine scriptEditor;

        public MainEngine()
        {
            inputValidator = new InputValidator();

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

        public int GetNextZoneID()
        {
            MapInfoFile mapInfo = mapEditor.GetMapInfoFile();
            return mapInfo.ZoneData.Count;
        }

        public int GetNextAreaID()
        {
            MapInfoFile mapInfo = mapEditor.GetMapInfoFile();
            return mapInfo.ZoneData.Max(z => z.AreaID) + 1;
        }

        public List<ClonableMapInfoData> GetClonableMapInfoData()
        {
            MapInfoFile mapInfo = mapEditor.GetMapInfoFile();
            List<MessageFile> messageFiles = messageEditor.GetMessageFiles()[FileConstants.MessageEnglishBundleKey];
            MessageFile messageFile = messageFiles.Where(f => f.FileName == "english_dp_fld_areaname").First();
            return mapInfo.ZoneData.Select(z => new ClonableMapInfoData(z.ZoneID, messageFile.Labels.Where(l => l.Name == z.PokePlaceName).First().Words[0].Data)).ToList();
        }

        public List<string> ValidateInput(InputData inputData)
        {
            return inputValidator.ValidateInput(inputData);
        }

        public void InsertNewMapInfo(InputData inputData)
        {
            mapEditor.InsertNewMapInfo(inputData);
            mapEditor.SaveMapFilesInBasePath();
        }
    }
}
