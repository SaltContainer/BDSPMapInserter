using AssetsTools.NET;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Data;
using BDSPMapInserter.Engine.MessageEditor.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDSPMapInserter.Engine.MapEditor.Model;

namespace BDSPMapInserter.Engine.MapEditor
{
    internal class MapEditorEngine
    {
        private List<MasterDatasFile> masterDatasFiles;

        public MapEditorEngine()
        {
            masterDatasFiles = new List<MasterDatasFile>();
        }

        public List<MasterDatasFile> GetMasterDatasFiles()
        {
            if (!AreMasterDatasFilesLoaded()) LoadMasterDatasFiles();
            return masterDatasFiles;
        }

        public void SetMasterDatasFiles(Dictionary<string, List<MasterDatasFile>> masterDatasFiles)
        {
            foreach (var masterDatasFile in masterDatasFiles)
            {
                BundleManipulator.SetFilesToBundle(masterDatasFile.Key, ConvertFromMasterDatasFiles(masterDatasFile.Value));
            }
        }

        public bool SaveMasterDatasFiles(string path)
        {
            return BundleManipulator.SaveBundles(new List<string>() { FileConstants.DprMasterDatasBundleKey }, path);
        }

        public bool SaveMasterDatasFilesInBasePath()
        {
            return BundleManipulator.SaveBundlesInBasePath(new List<string>() { FileConstants.DprMasterDatasBundleKey });
        }

        public bool AreMasterDatasFilesLoaded()
        {
            return BundleManipulator.IsBundleLoaded(FileConstants.DprMasterDatasBundleKey);
        }

        private bool LoadMasterDatasFiles()
        {
            bool result = BundleManipulator.LoadBundles(new List<string>() { FileConstants.DprMasterDatasBundleKey });
            if (result)
            {
                var files = BundleManipulator.GetFilesOfBundle(FileConstants.DprMasterDatasBundleKey);
                foreach (var file in files)
                {
                    if (FileConstants.Bundles[FileConstants.DprMasterDatasBundleKey].Files.Contains(file.Value["m_Name"].GetValue().AsString()) && 
                        file.Value["Data"] != null)
                    {
                        masterDatasFiles.Add(ConvertToMasterDatasFile(file.Key, file.Value));
                    }
                }
            }
            return result;
        }

        private MasterDatasFile ConvertToMasterDatasFile(long pathId, AssetTypeValueField root)
        {
            string name = root["m_Name"].GetValue().AsString();
            return new MasterDatasFile(new List<bool>(), name, pathId);
        }

        private List<JObject> ConvertFromMasterDatasFiles(List<MasterDatasFile> masterdatasFiles)
        {
            List<JObject> json = new List<JObject>();

            foreach (MasterDatasFile masterdatasFile in masterdatasFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", masterdatasFile.PathID),
                    new JProperty("FileName", masterdatasFile.FileName),
                    new JProperty("Data", new JArray())
                ));
            }

            return json;
        }
    }
}
