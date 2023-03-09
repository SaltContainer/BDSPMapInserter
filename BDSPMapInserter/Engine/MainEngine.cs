using BDSPMapInserter.Data;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Engine.MapEditor;
using BDSPMapInserter.Engine.MapEditor.Model;
using BDSPMapInserter.Engine.MessageEditor;
using BDSPMapInserter.Engine.MessageEditor.Model;
using BDSPMapInserter.Engine.ScriptEditor;
using BDSPMapInserter.Engine.ScriptEditor.Model;
using BDSPMapInserter.UI.Model;
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

        public void InsertNewMapInfo(int idToClone, int newZoneId, int newAreaId, long newZoneGrid, long newAttributeMatrix, long newAttributeMatrixEx)
        {
            mapEditor.InsertNewMapInfo(idToClone, newZoneId, newAreaId, newZoneGrid, newAttributeMatrix, newAttributeMatrixEx);
        }
    }
}
