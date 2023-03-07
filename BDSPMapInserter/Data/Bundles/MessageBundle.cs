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
    internal class MessageBundle : Bundle
    {
        public MessageBundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey) : base(assetsManager, bundle, bundleKey) { }

        protected override AssetsReplacer GenerateReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            // TODO: Add missing values
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());
            baseField["langID"].GetValue().Set(jfile["langID"].Value<int>());
            baseField["isKanji"].GetValue().Set(jfile["isKanji"].Value<int>());

            JArray jdatas = (JArray)jfile["labelDataArray"];
            var dataArray = baseField["labelDataArray"]["Array"];
            AdjustArrayLength(jdatas, dataArray);
            for (int i = 0; i < jdatas.Count; i++)
            {
                JToken jdata = jdatas[i];
                baseField["labelDataArray"][0][i]["labelIndex"].GetValue().Set(jdata["labelIndex"].Value<int>());
                baseField["labelDataArray"][0][i]["arrayIndex"].GetValue().Set(jdata["labelIndex"].Value<int>());
                baseField["labelDataArray"][0][i]["labelName"].GetValue().Set(jdata["labelName"].ToString());

                JArray jwords = (JArray)jdata["wordDataArray"];
                var wordArray = baseField["wordDataArray"]["Array"];
                AdjustArrayLength(jwords, wordArray);
                for (int j = 0; j < jwords.Count; j++)
                {
                    JToken jword = jwords[i];
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["str"].GetValue().Set(jword["str"].ToString());
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["strWidth"].GetValue().Set(jword["strWidth"].Value<float>());
                }
            }

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
