using AssetsTools.NET.Extra;
using AssetsTools.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Data.Bundles
{
    internal class GameSettingsBundle : Bundle
    {
        public GameSettingsBundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey) : base(assetsManager, bundle, bundleKey) { }

        protected override AssetsReplacer GenerateReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            switch (baseField["ClassID"].GetValue().AsInt())
            {
                case 0:
                    return GenerateMapInfoReplacerAtFile(baseField, jfile);
                case 11:
                    return GenerateAttributeReplacerAtFile(baseField, jfile);
                default:
                    return null;
            }
        }

        private AssetsReplacer GenerateMapInfoReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_GameObject"]["m_FileID"].GetValue().Set(0);
            baseField["m_GameObject"]["m_PathID"].GetValue().Set(0);
            baseField["m_Enabled"].GetValue().Set(1);
            baseField["m_Script"]["m_FileID"].GetValue().Set(0);
            baseField["m_Script"]["m_PathID"].GetValue().Set(-7230471827527798639);

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());

            JArray jzoneDatas = (JArray)jfile["ZoneData"];
            var zoneDataArray = baseField["ZoneData"]["Array"];
            AdjustArrayLength(jzoneDatas, zoneDataArray);
            for (int i = 0; i < jzoneDatas.Count; i++)
            {
                JToken jzoneData = jzoneDatas[i];

                baseField["ZoneData"][0][i]["Caption"].GetValue().Set(jzoneData["Caption"].ToString());
                baseField["ZoneData"][0][i]["MSLabel"].GetValue().Set(jzoneData["MSLabel"].ToString());
                baseField["ZoneData"][0][i]["PokePlaceName"].GetValue().Set(jzoneData["PokePlaceName"].ToString());
                baseField["ZoneData"][0][i]["FlyingPlaceName"].GetValue().Set(jzoneData["FlyingPlaceName"].ToString());
                baseField["ZoneData"][0][i]["MapType"].GetValue().Set(jzoneData["MapType"].Value<int>());
                baseField["ZoneData"][0][i]["IsField"].GetValue().Set(jzoneData["IsField"].Value<int>());
                baseField["ZoneData"][0][i]["LandmarkType"].GetValue().Set(jzoneData["LandmarkType"].Value<int>());
                baseField["ZoneData"][0][i]["MiniMapOffset"]["x"].GetValue().Set(jzoneData["MiniMapOffset"]["x"].Value<float>());
                baseField["ZoneData"][0][i]["MiniMapOffset"]["y"].GetValue().Set(jzoneData["MiniMapOffset"]["y"].Value<float>());
                baseField["ZoneData"][0][i]["Bicycle"].GetValue().Set(jzoneData["Bicycle"].Value<int>());
                baseField["ZoneData"][0][i]["Escape"].GetValue().Set(jzoneData["Escape"].Value<int>());
                baseField["ZoneData"][0][i]["Fly"].GetValue().Set(jzoneData["Fly"].Value<int>());
                baseField["ZoneData"][0][i]["BattleSearcher"].GetValue().Set(jzoneData["BattleSearcher"].Value<int>());
                baseField["ZoneData"][0][i]["TureAruki"].GetValue().Set(jzoneData["TureAruki"].Value<int>());
                baseField["ZoneData"][0][i]["KuruKuru"].GetValue().Set(jzoneData["KuruKuru"].Value<int>());
                baseField["ZoneData"][0][i]["Fall"].GetValue().Set(jzoneData["Fall"].Value<int>());

                JArray jbattlebgs = (JArray)jzoneData["BattleBg"];
                var battlebgArray = baseField["ZoneData"][0][i]["BattleBg"]["Array"];
                AdjustArrayLength(jbattlebgs, battlebgArray);
                for (int j = 0; j < jbattlebgs.Count; j++)
                {
                    JToken jbattlebg = jbattlebgs[j];
                    baseField["ZoneData"][0][i]["BattleBg"][0][j].GetValue().Set(jbattlebg.Value<int>());
                }

                baseField["ZoneData"][0][i]["ZoneID"].GetValue().Set(jzoneData["ZoneID"].Value<int>());
                baseField["ZoneData"][0][i]["AreaID"].GetValue().Set(jzoneData["AreaID"].Value<int>());
                baseField["ZoneData"][0][i]["ZoneGrid"]["m_FileID"].GetValue().Set(jzoneData["ZoneGrid"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["ZoneGrid"]["m_PathID"].GetValue().Set(jzoneData["ZoneGrid"]["m_PathID"].Value<long>());
                baseField["ZoneData"][0][i]["Attribute"]["m_FileID"].GetValue().Set(jzoneData["Attribute"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["Attribute"]["m_PathID"].GetValue().Set(jzoneData["Attribute"]["m_PathID"].Value<long>());
                baseField["ZoneData"][0][i]["AttributeEx"]["m_FileID"].GetValue().Set(jzoneData["AttributeEx"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["AttributeEx"]["m_PathID"].GetValue().Set(jzoneData["AttributeEx"]["m_PathID"].Value<long>());
                baseField["ZoneData"][0][i]["SubAttribute"]["m_FileID"].GetValue().Set(jzoneData["SubAttribute"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["SubAttribute"]["m_PathID"].GetValue().Set(jzoneData["SubAttribute"]["m_PathID"].Value<long>());
                baseField["ZoneData"][0][i]["SubAttributeEx"]["m_FileID"].GetValue().Set(jzoneData["SubAttributeEx"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["SubAttributeEx"]["m_PathID"].GetValue().Set(jzoneData["SubAttributeEx"]["m_PathID"].Value<long>());

                JArray jbgms = (JArray)jzoneData["BGM"];
                var bgmArray = baseField["ZoneData"][0][i]["BGM"]["Array"];
                AdjustArrayLength(jbgms, bgmArray);
                for (int j = 0; j < jbgms.Count; j++)
                {
                    JToken jbgm = jbgms[j];
                    baseField["ZoneData"][0][i]["BGM"][0][j].GetValue().Set(jbgm.ToString());
                }

                baseField["ZoneData"][0][i]["EnvironmentalSound"].GetValue().Set(jzoneData["EnvironmentalSound"].ToString());
                baseField["ZoneData"][0][i]["Weather"].GetValue().Set(jzoneData["Weather"].Value<int>());
                baseField["ZoneData"][0][i]["RenderSettings"]["m_FileID"].GetValue().Set(jzoneData["RenderSettings"]["m_FileID"].Value<int>());
                baseField["ZoneData"][0][i]["RenderSettings"]["m_PathID"].GetValue().Set(jzoneData["RenderSettings"]["m_PathID"].Value<long>());
                baseField["ZoneData"][0][i]["ReflectionCamera"].GetValue().Set(jzoneData["ReflectionCamera"].Value<int>());
                baseField["ZoneData"][0][i]["FixedTime"].GetValue().Set(jzoneData["FixedTime"].Value<int>());
                baseField["ZoneData"][0][i]["AssetBundleName"].GetValue().Set(jzoneData["AssetBundleName"].ToString());
                baseField["ZoneData"][0][i]["RoomPanCamera"].GetValue().Set(jzoneData["RoomPanCamera"].Value<int>());

                JArray jlocators = (JArray)jzoneData["Locators"];
                var locatorsArray = baseField["ZoneData"][0][i]["Locators"]["Array"];
                AdjustArrayLength(jlocators, locatorsArray);
                for (int j = 0; j < jlocators.Count; j++)
                {
                    JToken jlocator = jlocators[j];
                    baseField["ZoneData"][0][i]["Locators"][0][j]["x"].GetValue().Set(jlocator["x"].ToString());
                    baseField["ZoneData"][0][i]["Locators"][0][j]["y"].GetValue().Set(jlocator["y"].ToString());
                    baseField["ZoneData"][0][i]["Locators"][0][j]["z"].GetValue().Set(jlocator["z"].ToString());
                    baseField["ZoneData"][0][i]["Locators"][0][j]["w"].GetValue().Set(jlocator["w"].ToString());
                }
            }

            JArray jcameras = (JArray)jfile["Camera"];
            var cameraArray = baseField["Camera"]["Array"];
            AdjustArrayLength(jcameras, cameraArray);
            for (int i = 0; i < jcameras.Count; i++)
            {
                JToken jcamera = jcameras[i];

                baseField["Camera"][0][i]["ariaID"].GetValue().Set(jcamera["ariaID"].Value<int>());
                baseField["Camera"][0][i]["pitch"].GetValue().Set(jcamera["pitch"].Value<float>());
                baseField["Camera"][0][i]["fov"].GetValue().Set(jcamera["fov"].Value<float>());
                baseField["Camera"][0][i]["targetRange"].GetValue().Set(jcamera["targetRange"].Value<float>());
                baseField["Camera"][0][i]["panDistance"].GetValue().Set(jcamera["panDistance"].Value<float>());
                baseField["Camera"][0][i]["panPitch"].GetValue().Set(jcamera["panPitch"].Value<float>());
                baseField["Camera"][0][i]["panFov"].GetValue().Set(jcamera["panFov"].Value<float>());
                baseField["Camera"][0][i]["panpos_useflag"].GetValue().Set(jcamera["panpos_useflag"].Value<int>());
                baseField["Camera"][0][i]["panMinposY"].GetValue().Set(jcamera["panMinposY"].Value<float>());
                baseField["Camera"][0][i]["panMaxposY"].GetValue().Set(jcamera["panMaxposY"].Value<float>());
                baseField["Camera"][0][i]["panMinposZ"].GetValue().Set(jcamera["panMinposZ"].Value<float>());
                baseField["Camera"][0][i]["panMaxposZ"].GetValue().Set(jcamera["panMaxposZ"].Value<float>());
                baseField["Camera"][0][i]["defocusStart"].GetValue().Set(jcamera["defocusStart"].Value<float>());
                baseField["Camera"][0][i]["defocusEnd"].GetValue().Set(jcamera["defocusEnd"].Value<float>());
                baseField["Camera"][0][i]["distance"].GetValue().Set(jcamera["distance"].Value<float>());
            }

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }

        private AssetsReplacer GenerateAttributeReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_GameObject"]["m_FileID"].GetValue().Set(0);
            baseField["m_GameObject"]["m_PathID"].GetValue().Set(0);
            baseField["m_Enabled"].GetValue().Set(1);
            baseField["m_Script"]["m_FileID"].GetValue().Set(0);
            baseField["m_Script"]["m_PathID"].GetValue().Set(8862252896882353750);

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());

            JArray jAttributes = (JArray)jfile["Attributes"];
            var attributeArray = baseField["Attributes"]["Array"];
            AdjustArrayLength(jAttributes, attributeArray);
            for (int i = 0; i < jAttributes.Count; i++)
            {
                JToken jAttribute = jAttributes[i];
                baseField["Attributes"][0][i].GetValue().Set(jAttribute.Value<long>());
            }

            baseField["Width"].GetValue().Set(jfile["Width"].ToString());

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
