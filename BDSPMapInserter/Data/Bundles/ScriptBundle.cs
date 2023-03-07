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
    internal class ScriptBundle : Bundle
    {
        public ScriptBundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey) : base(assetsManager, bundle, bundleKey) { }

        protected override AssetsReplacer GenerateReplacerAtFile(AssetTypeValueField baseField, JObject jfile)
        {
            // TODO: Add missing values
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());

            baseField["m_Name"].GetValue().Set(jfile["FileName"].ToString());

            JArray jscripts = (JArray)jfile["Scripts"];
            var scriptArray = baseField["Scripts"]["Array"];
            AdjustArrayLength(jscripts, scriptArray);
            for (int i = 0; i < jscripts.Count; i++)
            {
                JToken jscript = jscripts[i];
                baseField["Scripts"][0][i]["Label"].GetValue().Set(jscript["Label"].ToString());

                JArray jcommands = (JArray)jscript["Commands"];
                var commandArray = baseField["Scripts"][0][i]["Commands"]["Array"];
                AdjustArrayLength(jcommands, commandArray);
                for (int j = 0; j < jcommands.Count; j++)
                {
                    JToken jcommand = jcommands[j];

                    JArray jargs = (JArray)jcommand["Arg"];
                    var argArray = baseField["Scripts"][0][i]["Commands"][0][j]["Arg"]["Array"];
                    AdjustArrayLength(jargs, argArray);
                    for (int k = 0; k < jargs.Count; k++)
                    {
                        JToken jarg = jargs[k];
                        baseField["Scripts"][0][i]["Commands"][0][j]["Arg"][0][k]["argType"].GetValue().Set(jarg["argType"]);
                        baseField["Scripts"][0][i]["Commands"][0][j]["Arg"][0][k]["data"].GetValue().Set(jarg["data"]);
                    }
                }
            }

            JArray jstrings = (JArray)jfile["StrList"];
            var stringArray = baseField["StrList"]["Array"];
            AdjustArrayLength(jstrings, stringArray);
            for (int i = 0; i < jstrings.Count; i++)
            {
                JToken jstring = jstrings[i];
                baseField["StrList"][0][i].GetValue().Set(jstring.ToString());
            }

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }
    }
}
