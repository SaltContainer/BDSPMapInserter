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
using BDSPMapInserter.Data.Bundles;

namespace BDSPMapInserter.Data
{
    internal static class BundleManipulator
    {
        private static AssetsManager assetsManager;
        private static BundleDecompressor bundleDecompressor;
        private static Dictionary<string, Bundle> bundles;
        private static string basePath;

        static BundleManipulator()
        {
            assetsManager = new AssetsManager();
            bundleDecompressor = new BundleDecompressor(assetsManager);
            bundles = new Dictionary<string, Bundle>();
            basePath = "";
        }

        public static bool IsBundleLoaded(string bundle)
        {
            return bundles.ContainsKey(bundle);
        }

        public static bool LoadBundles(List<string> bundles)
        {
            try
            {
                foreach (var entry in bundles)
                {
                    if (!IsBundleLoaded(entry))
                    {
                        switch (entry)
                        {
                            case FileConstants.ScriptDataBundleKey:
                                BundleManipulator.bundles.Add(entry, new ScriptBundle(assetsManager, bundleDecompressor.LoadAndDecompressFile(Path.Combine(basePath, FileConstants.Bundles[entry].FullPath)), entry));
                                break;
                            case FileConstants.DprMasterDatasBundleKey:
                                BundleManipulator.bundles.Add(entry, new MasterDatasBundle(assetsManager, bundleDecompressor.LoadAndDecompressFile(Path.Combine(basePath, FileConstants.Bundles[entry].FullPath)), entry));
                                break;
                            case FileConstants.GameSettingsBundleKey:
                                BundleManipulator.bundles.Add(entry, new GameSettingsBundle(assetsManager, bundleDecompressor.LoadAndDecompressFile(Path.Combine(basePath, FileConstants.Bundles[entry].FullPath)), entry));
                                break;
                            default:
                                BundleManipulator.bundles.Add(entry, new MessageBundle(assetsManager, bundleDecompressor.LoadAndDecompressFile(Path.Combine(basePath, FileConstants.Bundles[entry].FullPath)), entry));
                                break;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error reading or decompressing one of the bundles. Full Exception: " + ex.Message, "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool SaveAllBundles(string directory)
        {
            try
            {
                foreach (var entry in bundles)
                {
                    SaveBundleInFile(entry.Key, Path.Combine(directory, FileConstants.Bundles[entry.Key].FullPath));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error writing one of the bundles. Full Exception: " + ex.Message, "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool SaveBundles(List<string> bundles, string directory)
        {
            try
            {
                foreach (var entry in bundles)
                {
                    SaveBundleInFile(entry, Path.Combine(directory, FileConstants.Bundles[entry].FullPath));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error writing one of the bundles. Full Exception: " + ex.Message, "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool SaveBundlesInBasePath(List<string> bundles)
        {
            return SaveBundles(bundles, basePath);
        }

        public static bool SaveBundle(string bundleKey, string directory)
        {
            try
            {
                SaveBundleInFile(bundleKey, Path.Combine(directory, FileConstants.Bundles[bundleKey].FullPath));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error writing one of the bundles. Full Exception: " + ex.Message, "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void SaveBundleInFile(string bundleKey, string outFileName)
        {
            Bundle data = bundles[bundleKey];
            BundleFileInstance bundleInstance = data.GetBundle();
            string cabDirName = FileConstants.Bundles[bundleKey].CabDirectory;

            BundleReplacerFromMemory bundleReplacer = new BundleReplacerFromMemory(cabDirName, cabDirName, true, data.GetNewData(), -1);
            AssetsFileWriter bundleWriter = new AssetsFileWriter(File.OpenWrite(outFileName));
            bundleInstance.file.Write(bundleWriter, new List<BundleReplacer>() { bundleReplacer });

            bundleWriter.Close();
        }

        public static Dictionary<long, AssetTypeValueField> GetFilesOfBundle(string bundleKey)
        {
            return bundles[bundleKey].GetFilesInBundle();
        }

        public static AssetTypeValueField GetFileOfBundle(string bundleKey, string fileName)
        {
            return bundles[bundleKey].GetFileInBundle(fileName);
        }

        public static string GetBasePath()
        {
            return basePath;
        }

        public static void SetFilesToBundle(string bundleKey, List<JObject> files)
        {
            bundles[bundleKey].SetFilesInBundle(files);
        }

        public static long AddNewFileToBundle(string bundleKey, JObject data, int typeId, ushort classId, string container)
        {
            return bundles[bundleKey].AddAsset(typeId, classId, data, container);
        }

        public static bool SetBasePath(string path)
        {
            if (Directory.Exists(Path.Combine(path, "Data")))
            {
                basePath = path;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Bundle GetBundle(string bundleKey)
        {
            if (IsBundleLoaded(bundleKey)) return bundles[bundleKey];
            else return null;
        }
    }
}
