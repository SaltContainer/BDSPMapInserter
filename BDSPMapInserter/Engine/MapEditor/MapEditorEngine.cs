using AssetsTools.NET;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDSPMapInserter.Engine.MapEditor.Model;
using AssetsTools.NET.Extra;
using System.Drawing;
using static BDSPMapInserter.Engine.MapEditor.Model.ZoneData;

namespace BDSPMapInserter.Engine.MapEditor
{
    internal class MapEditorEngine
    {
        private List<MasterDatasFile> masterDatasFiles;
        private MapInfoFile mapInfoFile;
        private List<AttributeFile> attributeFiles;

        public MapEditorEngine()
        {
            masterDatasFiles = new List<MasterDatasFile>();
            mapInfoFile = null;
            attributeFiles = new List<AttributeFile>();
        }

        public List<MasterDatasFile> GetMasterDatasFiles()
        {
            if (!AreMasterDatasFilesLoaded()) LoadMasterDatasFiles();
            return masterDatasFiles;
        }

        public MapInfoFile GetMapInfoFile()
        {
            if (!AreGameSettingsFilesLoaded()) LoadGameSettingsFiles();
            return mapInfoFile;
        }

        public List<AttributeFile> GetAttributeFiles()
        {
            if (!AreGameSettingsFilesLoaded()) LoadGameSettingsFiles();
            return attributeFiles;
        }

        public void SetMasterDatasFiles(List<MasterDatasFile> masterDatasFiles)
        {
            BundleManipulator.SetFilesToBundle(FileConstants.DprMasterDatasBundleKey, ConvertFromMasterDatasFiles(masterDatasFiles));
        }

        public void SetMapInfoFile(MapInfoFile mapInfoFile)
        {
            BundleManipulator.SetFilesToBundle(FileConstants.GameSettingsBundleKey, ConvertFromMapInfoFile(mapInfoFile));
        }

        public void SetAttributeFiles(List<AttributeFile> attributeFiles)
        {
            BundleManipulator.SetFilesToBundle(FileConstants.GameSettingsBundleKey, ConvertFromAttributeFiles(attributeFiles));
        }

        public bool SaveMapFiles(string path)
        {
            return BundleManipulator.SaveBundles(new List<string>() {
                FileConstants.DprMasterDatasBundleKey,
                FileConstants.GameSettingsBundleKey }, path);
        }

        public bool SaveMapFilesInBasePath()
        {
            return BundleManipulator.SaveBundlesInBasePath(new List<string>(){
                FileConstants.DprMasterDatasBundleKey,
                FileConstants.GameSettingsBundleKey });
        }

        public bool AreMasterDatasFilesLoaded()
        {
            return BundleManipulator.IsBundleLoaded(FileConstants.DprMasterDatasBundleKey);
        }

        public bool AreGameSettingsFilesLoaded()
        {
            return BundleManipulator.IsBundleLoaded(FileConstants.GameSettingsBundleKey);
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
                        Bundle bundle = BundleManipulator.GetBundle(FileConstants.DprMasterDatasBundleKey);
                        masterDatasFiles.Add(ConvertToMasterDatasFile(file.Key, file.Value, bundle));
                    }
                }
            }
            return result;
        }

        private bool LoadGameSettingsFiles()
        {
            bool result = BundleManipulator.LoadBundles(new List<string>() { FileConstants.GameSettingsBundleKey });
            if (result)
            {
                var files = BundleManipulator.GetFilesOfBundle(FileConstants.GameSettingsBundleKey);
                foreach (var file in files)
                {
                    if (FileConstants.Bundles[FileConstants.GameSettingsBundleKey].Files.Contains(file.Value["m_Name"].GetValue().AsString()) &&
                        file.Value["Data"] != null)
                    {
                        if (file.Value["m_Name"].GetValue().AsString() == "MapInfo")
                        {
                            mapInfoFile = ConvertToMapInfoFile(file.Key, file.Value);
                        }
                        else
                        {
                            attributeFiles.Add(ConvertToAttributeFile(file.Key, file.Value));
                        }
                    }
                }
            }
            return result;
        }

        private MasterDatasFile ConvertToMasterDatasFile(long pathId, AssetTypeValueField root, Bundle bundle)
        {
            string name = root["m_Name"].GetValue().AsString();
            var file = bundle.GetAssetsFile();
            int classId = AssetHelper.GetScriptIndex(file.file, file.table.GetAssetInfo(pathId));
            return new MasterDatasFile(new List<bool>(), classId, name, pathId);
        }

        private MapInfoFile ConvertToMapInfoFile(long pathId, AssetTypeValueField root)
        {
            string name = root["m_Name"].GetValue().AsString();
            
            List<ZoneData> zoneDatas = new List<ZoneData>();
            foreach (var jzoneData in root["ZoneData"][0].GetChildrenList())
            {
                ZoneData zoneData = new ZoneData
                {
                    Caption = jzoneData["Caption"].GetValue().AsString(),
                    MSLabel = jzoneData["MSLabel"].GetValue().AsString(),
                    PokePlaceName = jzoneData["PokePlaceName"].GetValue().AsString(),
                    FlyingPlaceName = jzoneData["FlyingPlaceName"].GetValue().AsString(),
                    MapType = jzoneData["MapType"].GetValue().AsInt(),
                    IsField = jzoneData["IsField"].GetValue().AsInt(),
                    LandmarkType = jzoneData["LandmarkType"].GetValue().AsInt(),
                    MiniMapOffset = new PointF
                    (
                        jzoneData["MiniMapOffset"]["x"].GetValue().AsFloat(),
                        jzoneData["MiniMapOffset"]["y"].GetValue().AsFloat()
                    ),
                    Bicycle = jzoneData["Bicycle"].GetValue().AsInt(),
                    Escape = jzoneData["Escape"].GetValue().AsInt(),
                    Fly = jzoneData["Fly"].GetValue().AsInt(),
                    BattleSearcher = jzoneData["BattleSearcher"].GetValue().AsInt(),
                    TureAruki = jzoneData["TureAruki"].GetValue().AsInt(),
                    KuruKuru = jzoneData["KuruKuru"].GetValue().AsInt(),
                    Fall = jzoneData["Fall"].GetValue().AsInt(),
                    BattleBg = jzoneData["BattleBg"][0].GetChildrenList().Select(b => b.GetValue().AsInt()).ToList(),
                    ZoneID = jzoneData["ZoneID"].GetValue().AsInt(),
                    AreaID = jzoneData["AreaID"].GetValue().AsInt(),
                    ZoneGridPathID = jzoneData["ZoneGrid"].GetValue().AsInt64(),
                    AttributePathID = jzoneData["Attribute"].GetValue().AsInt64(),
                    AttributeExPathID = jzoneData["AttributeEx"].GetValue().AsInt64(),
                    SubAttributePathID = jzoneData["SubAttribute"].GetValue().AsInt64(),
                    SubAttributeExPathID = jzoneData["SubAttributeEx"].GetValue().AsInt64(),
                    BGM = jzoneData["BGM"][0].GetChildrenList().Select(b => b.GetValue().AsString()).ToList(),
                    EnvironmentalSound = jzoneData["EnvironmentalSound"].GetValue().AsString(),
                    Weather = jzoneData["Weather"].GetValue().AsInt(),
                    RenderSettingsPathID = jzoneData["RenderSettings"].GetValue().AsInt64(),
                    ReflectionCamera = jzoneData["ReflectionCamera"].GetValue().AsInt(),
                    FixedTime = jzoneData["FixedTime"].GetValue().AsInt(),
                    AssetBundleName = jzoneData["AssetBundleName"].GetValue().AsString(),
                    RoomPanCamera = jzoneData["RoomPanCamera"].GetValue().AsInt(),
                    Locators = jzoneData["Locators"][0].GetChildrenList().Select(l =>
                        new Locator
                        (
                            l["x"].GetValue().AsFloat(),
                            l["y"].GetValue().AsFloat(),
                            l["z"].GetValue().AsFloat(),
                            l["w"].GetValue().AsFloat()
                        )
                    ).ToList()
                };

                zoneDatas.Add(zoneData);
            }

            List<Camera> cameras = new List<Camera>();
            foreach (var jcamera in root["Camera"][0].GetChildrenList())
            {
                Camera camera = new Camera
                {
                    AreaID = jcamera["ariaID"].GetValue().AsInt(),
                    Pitch = jcamera["pitch"].GetValue().AsFloat(),
                    FOV = jcamera["fov"].GetValue().AsFloat(),
                    TargetRange = jcamera["targetRange"].GetValue().AsFloat(),
                    PanDistance = jcamera["panDistance"].GetValue().AsFloat(),
                    PanPitch = jcamera["panPitch"].GetValue().AsFloat(),
                    PanFOV = jcamera["panFov"].GetValue().AsFloat(),
                    PanPosUseFlag = jcamera["panpos_useflag"].GetValue().AsInt(),
                    PanMinPosY = jcamera["panMinposY"].GetValue().AsFloat(),
                    PanMaxPosY = jcamera["panMaxposY"].GetValue().AsFloat(),
                    PanMinPosZ = jcamera["panMinposZ"].GetValue().AsFloat(),
                    PanMaxPosZ = jcamera["panMaxposZ"].GetValue().AsFloat(),
                    DefocusStart = jcamera["defocusStart"].GetValue().AsFloat(),
                    DefocusEnd = jcamera["defocusEnd"].GetValue().AsFloat(),
                    Distance = jcamera["distance"].GetValue().AsFloat()
                };

                cameras.Add(camera);
            }

            MapInfoFile result = new MapInfoFile(zoneDatas, cameras, name, pathId);
            return result;
        }

        private AttributeFile ConvertToAttributeFile(long pathId, AssetTypeValueField root)
        {
            string name = root["m_Name"].GetValue().AsString();
            int width = root["Width"].GetValue().AsInt();
            return new AttributeFile(root["Attributes"][0].GetChildrenList().Select(b => b.GetValue().AsInt64()).ToList(), width, name, pathId);
        }

        private List<JObject> ConvertFromMasterDatasFiles(List<MasterDatasFile> masterdatasFiles)
        {
            // TODO: Add missing values
            List<JObject> json = new List<JObject>();

            foreach (MasterDatasFile masterdatasFile in masterdatasFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", masterdatasFile.PathID),
                    new JProperty("ClassID", masterdatasFile.ClassID),
                    new JProperty("FileName", masterdatasFile.FileName),
                    new JProperty("Data", new JArray())
                ));
            }

            return json;
        }

        private List<JObject> ConvertFromMapInfoFile(MapInfoFile mapInfoFile)
        {
            // TODO: Add missing values
            List<JObject> json = new List<JObject>
            {
                new JObject(
                    new JProperty("PathID", mapInfoFile.PathID),
                    new JProperty("ClassID", 0),
                    new JProperty("FileName", mapInfoFile.FileName),
                    new JProperty("ZoneData",
                        new JArray(
                            from z in mapInfoFile.ZoneData
                            select new JObject(
                                new JProperty("Caption", z.Caption),
                                new JProperty("MSLabel", z.MSLabel),
                                new JProperty("PokePlaceName", z.PokePlaceName),
                                new JProperty("FlyingPlaceName", z.FlyingPlaceName),
                                new JProperty("MapType", z.MapType),
                                new JProperty("IsField", z.IsField),
                                new JProperty("LandmarkType", z.LandmarkType),
                                new JProperty("MiniMapOffset",
                                    new JObject(
                                        new JProperty("x", z.MiniMapOffset.X),
                                        new JProperty("y", z.MiniMapOffset.Y)
                                    )
                                ),
                                new JProperty("Bicycle", z.Bicycle),
                                new JProperty("Escape", z.Escape),
                                new JProperty("Fly", z.Fly),
                                new JProperty("BattleSearcher", z.BattleSearcher),
                                new JProperty("TureAruki", z.TureAruki),
                                new JProperty("KuruKuru", z.KuruKuru),
                                new JProperty("Fall", z.Fall),
                                new JProperty("BattleBg",
                                    new JArray(
                                        from b in z.BattleBg
                                        select new JValue(b)
                                    )
                                ),
                                new JProperty("ZoneID", z.ZoneID),
                                new JProperty("AreaID", z.AreaID),
                                new JProperty("ZoneGrid",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.ZoneGridPathID)
                                    )
                                ),
                                new JProperty("Attribute",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.AttributePathID)
                                    )
                                ),
                                new JProperty("AttributeEx",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.AttributeExPathID)
                                    )
                                ),
                                new JProperty("SubAttribute",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.SubAttributePathID)
                                    )
                                ),
                                new JProperty("SubAttributeEx",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.SubAttributeExPathID)
                                    )
                                ),
                                new JProperty("BGM",
                                    new JArray(
                                        from b in z.BGM
                                        select new JValue(b)
                                    )
                                ),
                                new JProperty("EnvironmentalSound", z.EnvironmentalSound),
                                new JProperty("Weather", z.Weather),
                                new JProperty("RenderSettings",
                                    new JObject(
                                        new JProperty("m_FileID", 0),
                                        new JProperty("m_PathID", z.RenderSettingsPathID)
                                    )
                                ),
                                new JProperty("ReflectionCamera", z.ReflectionCamera),
                                new JProperty("FixedTime", z.FixedTime),
                                new JProperty("AssetBundleName", z.AssetBundleName),
                                new JProperty("RoomPanCamera", z.RoomPanCamera),
                                new JProperty("Locators",
                                    new JArray(
                                        from l in z.Locators
                                        select new JObject(
                                            new JProperty("x", l.GetX()),
                                            new JProperty("y", l.GetY()),
                                            new JProperty("z", l.GetZ()),
                                            new JProperty("w", l.GetW())
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new JProperty("Camera",
                        new JArray(
                            from c in mapInfoFile.Camera
                            select new JObject(
                                new JProperty("ariaID", c.AreaID),
                                new JProperty("pitch", c.Pitch),
                                new JProperty("fov", c.FOV),
                                new JProperty("targetRange", c.TargetRange),
                                new JProperty("panDistance", c.PanDistance),
                                new JProperty("panPitch", c.PanPitch),
                                new JProperty("panFov", c.PanFOV),
                                new JProperty("panpos_useflag", c.PanPosUseFlag),
                                new JProperty("panMinposY", c.PanMinPosY),
                                new JProperty("panMaxposY", c.PanMaxPosY),
                                new JProperty("panMinposZ", c.PanMinPosZ),
                                new JProperty("panMaxposZ", c.PanMaxPosZ),
                                new JProperty("defocusStart", c.DefocusStart),
                                new JProperty("defocusEnd", c.DefocusEnd),
                                new JProperty("distance", c.Distance)
                            )
                        )
                    )
                )
            };

            return json;
        }

        private List<JObject> ConvertFromAttributeFiles(List<AttributeFile> attributeFiles)
        {
            // TODO: Add missing values
            List<JObject> json = new List<JObject>();

            foreach (AttributeFile attributeFile in attributeFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", attributeFile.PathID),
                    new JProperty("ClassID", 11),
                    new JProperty("FileName", attributeFile.FileName),
                    new JProperty("Attributes",
                        new JArray(
                            from a in attributeFile.Attributes
                            select new JValue(a)
                        )
                    ),
                    new JProperty("Width", attributeFile.Width)
                ));
            }

            return json;
        }
    }
}
