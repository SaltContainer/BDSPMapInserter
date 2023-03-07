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
    internal class MasterDatasBundle : Bundle
    {
        public MasterDatasBundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey) : base(assetsManager, bundle, bundleKey) { }

        protected override AssetsReplacer GenerateReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_GameObject"]["m_FileID"].GetValue().Set(0);
            baseField["m_GameObject"]["m_PathID"].GetValue().Set(0);
            baseField["m_Enabled"].GetValue().Set(1);
            baseField["m_Script"]["m_FileID"].GetValue().Set(0);

            switch (jfile["ClassID"].Value<int>())
            {
                case 1:
                    baseField["m_Script"]["m_PathID"].GetValue().Set(-7768618106554099594);
                    break;
                case 24:
                    baseField["m_Script"]["m_PathID"].GetValue().Set(4652281892236711381);
                    break;
                case 30:
                    baseField["m_Script"]["m_PathID"].GetValue().Set(8423137904901713306);
                    break;
                default:
                    baseField["m_Script"]["m_PathID"].GetValue().Set(0);
                    break;
            }

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());

            JArray jdatas = (JArray)jfile["Data"];
            var dataArray = baseField["Data"]["Array"];
            AdjustArrayLength(jdatas, dataArray);

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
