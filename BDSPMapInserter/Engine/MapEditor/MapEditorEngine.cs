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
        private List<AttributeMatrixFile> attributeMatrixFiles;

        public MapEditorEngine()
        {
            masterDatasFiles = new List<MasterDatasFile>();
            mapInfoFile = null;
            attributeFiles = new List<AttributeFile>();
            attributeMatrixFiles = new List<AttributeMatrixFile>();
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

        public List<AttributeMatrixFile> GetAttributeMatrixFiles()
        {
            if (!AreGameSettingsFilesLoaded()) LoadGameSettingsFiles();
            return attributeMatrixFiles;
        }

        public void SetMasterDatasFiles(List<MasterDatasFile> masterDatasFiles)
        {
            List<MasterDatasFile> newFiles = new List<MasterDatasFile>();
            foreach (MasterDatasFile file in masterDatasFiles)
            {
                if (!this.masterDatasFiles.Exists(f => f.PathID == file.PathID))
                {
                    newFiles.Add(file);
                    string container = "";
                    switch (file.ClassID)
                    {
                        case 1:
                            container = string.Format("assets/md/mapwarpdata/mapwarp_{0}.asset", file.FileName.ToLower());
                            break;
                        case 24:
                            container = string.Format("assets/md/placedata/placedata_{0}.asset", file.FileName.ToLower());
                            break;
                        case 30:
                            container = string.Format("assets/md/stopdata/stopdata_{0}.asset", file.FileName.ToLower());
                            break;
                    }
                    BundleManipulator.AddNewFileToBundle(FileConstants.DprMasterDatasBundleKey, ConvertFromMasterDatasFiles(new List<MasterDatasFile>() { file })[0], 114, (ushort)file.ClassID, container);
                }
            }
            masterDatasFiles.RemoveAll(f => newFiles.Contains(f));
            BundleManipulator.SetFilesToBundle(FileConstants.DprMasterDatasBundleKey, ConvertFromMasterDatasFiles(masterDatasFiles));
        }

        public void SetMapInfoFile(MapInfoFile mapInfoFile)
        {
            BundleManipulator.SetFilesToBundle(FileConstants.GameSettingsBundleKey, ConvertFromMapInfoFile(mapInfoFile));
        }

        public void SetAttributeFiles(List<AttributeFile> attributeFiles)
        {
            List<AttributeFile> newFiles = new List<AttributeFile>();
            foreach (AttributeFile file in attributeFiles)
            {
                if (!this.attributeFiles.Exists(f => f.PathID == file.PathID))
                {
                    newFiles.Add(file);
                    BundleManipulator.AddNewFileToBundle(FileConstants.GameSettingsBundleKey, ConvertFromAttributeFiles(new List<AttributeFile>() { file })[0], 114, 0xffff, "");
                }
            }
            attributeFiles.RemoveAll(f => newFiles.Contains(f));
            BundleManipulator.SetFilesToBundle(FileConstants.GameSettingsBundleKey, ConvertFromAttributeFiles(attributeFiles));
        }

        public void SetAttributeMatrixFiles(List<AttributeMatrixFile> attributeMatrixFiles)
        {
            List<AttributeMatrixFile> newFiles = new List<AttributeMatrixFile>();
            foreach (AttributeMatrixFile file in attributeMatrixFiles)
            {
                if (!this.attributeMatrixFiles.Exists(f => f.PathID == file.PathID))
                {
                    newFiles.Add(file);
                    BundleManipulator.AddNewFileToBundle(FileConstants.GameSettingsBundleKey, ConvertFromAttributeMatrixFiles(new List<AttributeMatrixFile>() { file })[0], 114, 0xffff, "");
                }
            }
            attributeMatrixFiles.RemoveAll(f => newFiles.Contains(f));
            BundleManipulator.SetFilesToBundle(FileConstants.GameSettingsBundleKey, ConvertFromAttributeMatrixFiles(attributeMatrixFiles));
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

        public void InsertNewMapInfo(int idToClone, int newZoneId, int newAreaId, long newZoneGrid, long newAttributeMatrix, long newAttributeMatrixEx)
        {
            MapInfoFile mapInfo = GetMapInfoFile();

            ZoneData zoneData = new ZoneData(mapInfo.ZoneData[idToClone]);
            zoneData.ZoneID = newZoneId;
            zoneData.AreaID = newAreaId;
            zoneData.ZoneGridPathID = newZoneGrid;
            zoneData.AttributePathID = newAttributeMatrix;
            zoneData.AttributeExPathID = newAttributeMatrixEx;

            Camera camera = new Camera(mapInfo.Camera[idToClone]);
            camera.AreaID = newAreaId;
            
            mapInfo.ZoneData.Add(zoneData);
            mapInfo.Camera.Add(camera);
            SetMapInfoFile(mapInfo);
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
                        file.Value["Data"].GetChildrenCount() > 0)
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
                    if (FileConstants.Bundles[FileConstants.GameSettingsBundleKey].Files.Contains(file.Value["m_Name"].GetValue().AsString()))
                    {
                        if (file.Value["m_Name"].GetValue().AsString() == "MapInfo" &&
                            file.Value["ZoneData"].GetChildrenCount() > 0)
                        {
                            mapInfoFile = ConvertToMapInfoFile(file.Key, file.Value);
                        }
                        else if (file.Value["Attributes"].GetChildrenCount() > 0)
                        {
                            attributeFiles.Add(ConvertToAttributeFile(file.Key, file.Value));
                        }
                        else if (file.Value["AttributeBlocks"].GetChildrenCount() > 0)
                        {
                            attributeFiles.Add(ConvertToAttributeMatrixFile(file.Key, file.Value));
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
            foreach (var zoneDataChild in root["ZoneData"][0].GetChildrenList())
            {
                ZoneData zoneData = new ZoneData
                {
                    Caption = zoneDataChild["Caption"].GetValue().AsString(),
                    MSLabel = zoneDataChild["MSLabel"].GetValue().AsString(),
                    PokePlaceName = zoneDataChild["PokePlaceName"].GetValue().AsString(),
                    FlyingPlaceName = zoneDataChild["FlyingPlaceName"].GetValue().AsString(),
                    MapType = zoneDataChild["MapType"].GetValue().AsInt(),
                    IsField = zoneDataChild["IsField"].GetValue().AsInt(),
                    LandmarkType = zoneDataChild["LandmarkType"].GetValue().AsInt(),
                    MiniMapOffset = new PointF
                    (
                        zoneDataChild["MiniMapOffset"]["x"].GetValue().AsFloat(),
                        zoneDataChild["MiniMapOffset"]["y"].GetValue().AsFloat()
                    ),
                    Bicycle = zoneDataChild["Bicycle"].GetValue().AsInt(),
                    Escape = zoneDataChild["Escape"].GetValue().AsInt(),
                    Fly = zoneDataChild["Fly"].GetValue().AsInt(),
                    BattleSearcher = zoneDataChild["BattleSearcher"].GetValue().AsInt(),
                    TureAruki = zoneDataChild["TureAruki"].GetValue().AsInt(),
                    KuruKuru = zoneDataChild["KuruKuru"].GetValue().AsInt(),
                    Fall = zoneDataChild["Fall"].GetValue().AsInt(),
                    BattleBg = zoneDataChild["BattleBg"][0].GetChildrenList().Select(b => b.GetValue().AsInt()).ToList(),
                    ZoneID = zoneDataChild["ZoneID"].GetValue().AsInt(),
                    AreaID = zoneDataChild["AreaID"].GetValue().AsInt(),
                    ZoneGridPathID = zoneDataChild["ZoneGrid"]["m_PathID"].GetValue().AsInt64(),
                    AttributePathID = zoneDataChild["Attribute"]["m_PathID"].GetValue().AsInt64(),
                    AttributeExPathID = zoneDataChild["AttributeEx"]["m_PathID"].GetValue().AsInt64(),
                    SubAttributePathID = zoneDataChild["SubAttribute"]["m_PathID"].GetValue().AsInt64(),
                    SubAttributeExPathID = zoneDataChild["SubAttributeEx"]["m_PathID"].GetValue().AsInt64(),
                    BGM = zoneDataChild["BGM"][0].GetChildrenList().Select(b => b.GetValue().AsString()).ToList(),
                    EnvironmentalSound = zoneDataChild["EnvironmentalSound"].GetValue().AsString(),
                    Weather = zoneDataChild["Weather"].GetValue().AsInt(),
                    RenderSettingsPathID = zoneDataChild["RenderSettings"]["m_PathID"].GetValue().AsInt64(),
                    ReflectionCamera = zoneDataChild["ReflectionCamera"].GetValue().AsInt(),
                    FixedTime = zoneDataChild["FixedTime"].GetValue().AsInt(),
                    AssetBundleName = zoneDataChild["AssetBundleName"].GetValue().AsString(),
                    RoomPanCamera = zoneDataChild["RoomPanCamera"].GetValue().AsInt(),
                    Locators = zoneDataChild["Locators"][0].GetChildrenList().Select(l =>
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
            foreach (var cameraChild in root["Camera"][0].GetChildrenList())
            {
                Camera camera = new Camera
                {
                    AreaID = cameraChild["ariaID"].GetValue().AsInt(),
                    Pitch = cameraChild["pitch"].GetValue().AsFloat(),
                    FOV = cameraChild["fov"].GetValue().AsFloat(),
                    TargetRange = cameraChild["targetRange"].GetValue().AsFloat(),
                    PanDistance = cameraChild["panDistance"].GetValue().AsFloat(),
                    PanPitch = cameraChild["panPitch"].GetValue().AsFloat(),
                    PanFOV = cameraChild["panFov"].GetValue().AsFloat(),
                    PanPosUseFlag = cameraChild["panpos_useflag"].GetValue().AsInt(),
                    PanMinPosY = cameraChild["panMinposY"].GetValue().AsFloat(),
                    PanMaxPosY = cameraChild["panMaxposY"].GetValue().AsFloat(),
                    PanMinPosZ = cameraChild["panMinposZ"].GetValue().AsFloat(),
                    PanMaxPosZ = cameraChild["panMaxposZ"].GetValue().AsFloat(),
                    DefocusStart = cameraChild["defocusStart"].GetValue().AsFloat(),
                    DefocusEnd = cameraChild["defocusEnd"].GetValue().AsFloat(),
                    Distance = cameraChild["distance"].GetValue().AsFloat()
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

        private AttributeFile ConvertToAttributeMatrixFile(long pathId, AssetTypeValueField root)
        {
            string name = root["m_Name"].GetValue().AsString();
            int width = root["Width"].GetValue().AsInt();
            return new AttributeFile(root["AttributeBlocks"][0].GetChildrenList().Select(b => b["m_PathID"].GetValue().AsInt64()).ToList(), width, name, pathId);
        }

        private List<JObject> ConvertFromMasterDatasFiles(List<MasterDatasFile> masterdatasFiles)
        {
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

        private List<JObject> ConvertFromAttributeMatrixFiles(List<AttributeMatrixFile> attributeMatrixFiles)
        {
            List<JObject> json = new List<JObject>();

            foreach (AttributeMatrixFile attributeMatrixFile in attributeMatrixFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", attributeMatrixFile.PathID),
                    new JProperty("ClassID", 8),
                    new JProperty("FileName", attributeMatrixFile.FileName),
                    new JProperty("AttributeBlocks",
                        new JArray(
                            from p in attributeMatrixFile.AttributePathIDs
                            select new JObject(
                                new JProperty("m_FileID", 0),
                                new JProperty("m_PathID", p)
                            )
                        )
                    ),
                    new JProperty("Width", attributeMatrixFile.Width)
                ));
            }

            return json;
        }
    }
}
