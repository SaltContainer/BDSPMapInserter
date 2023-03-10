using AssetsTools.NET.Extra;
using AssetsTools.NET;
using BDSPMapInserter.Data.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace BDSPMapInserter.Data
{
    internal abstract class Bundle
    {
        protected AssetsManager assetsManager;
        protected string bundleKey;
        protected BundleFileInstance bundle;
        protected AssetsFileInstance assetsFile;
        protected byte[] newData;
        protected long nextPathId;

        public Bundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey)
        {
            this.assetsManager = assetsManager;
            this.bundle = bundle;
            this.bundleKey = bundleKey;

            assetsFile = assetsManager.LoadAssetsFileFromBundle(bundle, FileConstants.Bundles[bundleKey].CabDirectory);
            if (!assetsFile.file.typeTree.hasTypeTree)
                assetsManager.LoadClassDatabaseFromPackage(assetsFile.file.typeTree.unityVersion);
            
            this.nextPathId = assetsFile.table.assetFileInfo.Max(info => info.index) + 1;
        }

        public AssetTypeValueField GetFileInBundle(string fileName)
        {
            return GetBaseField(fileName);
        }

        public void SetFilesInBundle(List<JObject> files)
        {
            List<AssetsReplacer> replacers = GenerateReplacers(files);
            ReplaceInBundle(replacers);
        }

        public long AddAsset(int typeId, ushort classId, JObject data, string container)
        {
            AssetTypeTemplateField tempField = new AssetTypeTemplateField();
            byte[] assetBytes;

            if (assetsFile.file.typeTree.hasTypeTree)
            {
                var ttType = AssetHelper.FindTypeTreeTypeByID(assetsFile.file.typeTree, 114, classId);
                tempField.From0D(ttType, 0);
            }
            else
            {
                MessageBox.Show("No Type Tree???");
            }

            AssetTypeValueField baseField = ValueBuilder.DefaultValueFieldFromTemplate(tempField);
            assetBytes = baseField.WriteToByteArray();
            long pathId = GetNextPathID();
            AssetsReplacer replacer = new AssetsReplacerFromMemory(0, pathId, typeId, classId, assetBytes);
            ReplaceInBundle(new List<AssetsReplacer>() { replacer }); // TODO: New files just do not want to get added for some reason :/

            data["PathID"] = pathId;
            data["ClassID"] = classId;
            AssetsReplacer newReplacer = GenerateReplacerAtFile(baseField, data);
            ReplaceInBundle(new List<AssetsReplacer>() { newReplacer }); // TODO: This also doesn't change anything...

            if (container != null && container != "")
            {
                AssetsReplacer containerReplacer = GenerateFileContainerReplacer(pathId, container);
                ReplaceInBundle(new List<AssetsReplacer>() { containerReplacer });
            }

            return pathId;
        }

        public Dictionary<long, AssetTypeValueField> GetFilesInBundle()
        {
            Dictionary<long, AssetTypeValueField> files = new Dictionary<long, AssetTypeValueField>();
            foreach (var info in assetsFile.table.assetFileInfo)
            {
                AssetTypeValueField file = assetsManager.GetTypeInstance(assetsFile, info).GetBaseField();
                files.Add(info.index, file);
            }
            return files;
        }

        public AssetsFileInstance GetAssetsFile()
        {
            return assetsFile;
        }

        public BundleFileInstance GetBundle()
        {
            return bundle;
        }

        public byte[] GetNewData()
        {
            return newData;
        }

        protected abstract AssetsReplacer GenerateReplacerAtFile(AssetTypeValueField baseField, JObject jfile);

        protected void AdjustArrayLength(JArray jarray, AssetTypeValueField field)
        {
            if (jarray.Count <= field.childrenCount) field.SetChildrenList(field.children.Take(jarray.Count).ToArray());
            else
            {
                List<AssetTypeValueField> extra = new List<AssetTypeValueField>();
                for (int j = field.childrenCount; j < jarray.Count; j++) extra.Add(ValueBuilder.DefaultValueFieldFromArrayTemplate(field));
                field.SetChildrenList(field.children.Concat(extra).ToArray());
            }
        }

        private List<AssetsReplacer> GenerateReplacers(List<JObject> files)
        {
            List<AssetsReplacer> replacers = new List<AssetsReplacer>();
            foreach (var jfile in files)
            {
                AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(jfile["PathID"].Value<long>());
                AssetTypeValueField baseField = assetsManager.GetTypeInstance(assetsFile, fileInfo).GetBaseField();

                replacers.Add(GenerateReplacerAtFile(baseField, jfile));
            }

            return replacers;
        }

        private void ReplaceInBundle(List<AssetsReplacer> replacers)
        {
            if (replacers != null)
            {
                using (var stream = new MemoryStream())
                using (var writer = new AssetsFileWriter(stream))
                {
                    assetsFile.file.Write(writer, 0, replacers, 0);
                    newData = stream.ToArray();
                }
            }
        }

        private AssetsReplacerFromMemory GenerateFileContainerReplacer(long pathId, string container)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(1);
            AssetTypeValueField baseField = assetsManager.GetTypeInstance(assetsFile, fileInfo).GetBaseField();

            AssetTypeValueField preloadArray = baseField["m_PreloadTable"]["Array"];
            AssetTypeValueField newPreload = ValueBuilder.DefaultValueFieldFromArrayTemplate(preloadArray);
            newPreload["m_FileID"].GetValue().Set(0);
            newPreload["m_PathID"].GetValue().Set(pathId);
            int newItemIndex = preloadArray.childrenCount;
            preloadArray.SetChildrenList(preloadArray.children.Append(newPreload).ToArray());

            AssetTypeValueField containerArray = baseField["m_Container"]["Array"];
            AssetTypeValueField newContainer = ValueBuilder.DefaultValueFieldFromArrayTemplate(containerArray);
            newContainer[0].GetValue().Set(container);
            newContainer[1]["preloadIndex"].GetValue().Set(newItemIndex);
            newContainer[1]["preloadSize"].GetValue().Set(2);
            newContainer[1]["asset"]["m_FileID"].GetValue().Set(0);
            newContainer[1]["asset"]["m_PathID"].GetValue().Set(pathId);
            preloadArray.SetChildrenList(containerArray.children.Append(newContainer).ToArray());

            return new AssetsReplacerFromMemory(0, fileInfo.index, (int)fileInfo.curFileType, 0xffff, baseField.WriteToByteArray());
        }

        private AssetTypeValueField GetBaseField(string fileName)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(fileName);
            return assetsManager.GetTypeInstance(assetsFile, fileInfo).GetBaseField();
        }

        private long GetNextPathID()
        {
            return nextPathId++;
        }
    }
}
