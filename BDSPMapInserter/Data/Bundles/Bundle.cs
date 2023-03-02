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

namespace BDSPMapInserter.Data
{
    internal abstract class Bundle
    {
        protected AssetsManager assetsManager;
        protected string bundleKey;
        protected BundleFileInstance bundle;
        protected AssetsFileInstance assetsFile;
        protected byte[] newData;

        public Bundle(AssetsManager assetsManager, BundleFileInstance bundle, string bundleKey)
        {
            this.assetsManager = assetsManager;
            this.bundle = bundle;
            this.bundleKey = bundleKey;

            assetsFile = assetsManager.LoadAssetsFileFromBundle(bundle, FileConstants.Bundles[bundleKey].CabDirectory);
            if (!assetsFile.file.typeTree.hasTypeTree)
                assetsManager.LoadClassDatabaseFromPackage(assetsFile.file.typeTree.unityVersion);
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

        public void AddAsset(int typeId, ushort classId, JObject data)
        {
            AssetTypeTemplateField tempField = new AssetTypeTemplateField();
            byte[] assetBytes;

            if (assetsFile.file.typeTree.hasTypeTree)
            {
                var ttType = AssetHelper.FindTypeTreeTypeByID(assetsFile.file.typeTree, classId);
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
            ReplaceInBundle(new List<AssetsReplacer>() { replacer });

            data["PathID"] = pathId;
            AssetsReplacer newReplacer = GenerateReplacerAtFile(baseField, data);
            ReplaceInBundle(new List<AssetsReplacer>() { newReplacer });
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

        private AssetTypeValueField GetBaseField(string fileName)
        {
            AssetFileInfoEx fileInfo = assetsFile.table.GetAssetInfo(fileName);
            return assetsManager.GetTypeInstance(assetsFile, fileInfo).GetBaseField();
        }

        private long GetNextPathID()
        {
            return assetsFile.table.assetFileInfo.Max(info => info.index) + 1;
        }
    }
}
