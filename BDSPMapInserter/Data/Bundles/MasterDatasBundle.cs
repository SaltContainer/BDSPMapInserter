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
            // TODO: Add missing values
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());

            JArray jdatas = (JArray)jfile["Data"];
            var dataArray = baseField["Data"]["Array"];
            AdjustArrayLength(jdatas, dataArray);

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
