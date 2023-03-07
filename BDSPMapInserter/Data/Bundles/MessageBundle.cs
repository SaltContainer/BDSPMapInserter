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
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_GameObject"]["m_FileID"].GetValue().Set(0);
            baseField["m_GameObject"]["m_PathID"].GetValue().Set(0);
            baseField["m_Enabled"].GetValue().Set(1);
            baseField["m_Script"]["m_FileID"].GetValue().Set(0);
            baseField["m_Script"]["m_PathID"].GetValue().Set(3652094840918934080);

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
                baseField["labelDataArray"][0][i]["arrayIndex"].GetValue().Set(jdata["arrayIndex"].Value<int>());
                baseField["labelDataArray"][0][i]["labelName"].GetValue().Set(jdata["labelName"].ToString());

                if (jfile["FileName"].ToString().EndsWith("_dp_fld_areaname"))
                {
                    baseField["labelDataArray"][0][i]["styleInfo"]["styleIndex"].GetValue().Set(125);
                    baseField["labelDataArray"][0][i]["styleInfo"]["colorIndex"].GetValue().Set(-1);
                    baseField["labelDataArray"][0][i]["styleInfo"]["fontSize"].GetValue().Set(42);
                    baseField["labelDataArray"][0][i]["styleInfo"]["maxWidth"].GetValue().Set(504);
                    baseField["labelDataArray"][0][i]["styleInfo"]["controlID"].GetValue().Set(0);
                }
                else
                {
                    baseField["labelDataArray"][0][i]["styleInfo"]["styleIndex"].GetValue().Set(48);
                    baseField["labelDataArray"][0][i]["styleInfo"]["colorIndex"].GetValue().Set(-1);
                    baseField["labelDataArray"][0][i]["styleInfo"]["fontSize"].GetValue().Set(42);
                    baseField["labelDataArray"][0][i]["styleInfo"]["maxWidth"].GetValue().Set(672);
                    baseField["labelDataArray"][0][i]["styleInfo"]["controlID"].GetValue().Set(0);
                }

                JArray jattributes = new JArray(
                    from s in new List<int>() { -1, 0, 0, -1, 0 }
                    select new JValue(s)
                );
                var attributeArray = baseField["attributeValueArray"]["Array"];
                AdjustArrayLength(jattributes, attributeArray);
                for (int j = 0; j < jattributes.Count; j++)
                {
                    JToken jattribute = jattributes[j];
                    baseField["attributeValueArray"][0][j].GetValue().Set(jattribute.Value<int>());
                }

                JArray jtags = new JArray();
                var tagArray = baseField["tagDataArray"]["Array"];
                AdjustArrayLength(jtags, tagArray);

                JArray jwords = (JArray)jdata["wordDataArray"];
                var wordArray = baseField["wordDataArray"]["Array"];
                AdjustArrayLength(jwords, wordArray);
                for (int j = 0; j < jwords.Count; j++)
                {
                    JToken jword = jwords[i];
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["patternID"].GetValue().Set(0);
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["eventID"].GetValue().Set(7);
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["tagIndex"].GetValue().Set(-1);
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["tagValue"].GetValue().Set(0.0f);
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["str"].GetValue().Set(jword["str"].ToString());
                    baseField["labelDataArray"][0][i]["wordDataArray"][0][j]["strWidth"].GetValue().Set(jword["strWidth"].Value<float>());
                }
            }

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
