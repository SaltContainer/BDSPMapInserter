using AssetsTools.NET;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Data;
using BDSPMapInserter.Engine.MessageEditor.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BDSPMapInserter.Engine.MessageEditor
{
    internal class MessageEditorEngine
    {
        private Dictionary<string, List<MessageFile>> messageFiles;

        public MessageEditorEngine()
        {
            messageFiles = new Dictionary<string, List<MessageFile>>();
        }

        public Dictionary<string, List<MessageFile>> GetMessageFiles()
        {
            if (!AreMessageFilesLoaded()) LoadMessageFiles();
            return messageFiles;
        }

        public void SetMessageFiles(Dictionary<string, List<MessageFile>> messageFiles)
        {
            foreach (var messageFile in messageFiles)
            {
                BundleManipulator.SetFilesToBundle(messageFile.Key, ConvertFromMessageFiles(messageFile.Value));
            }
        }

        public bool SaveMessageFiles(string path)
        {
            return BundleManipulator.SaveBundles(FileConstants.MessageBundleKeys, path);
        }

        public bool SaveMessageFilesInBasePath()
        {
            return BundleManipulator.SaveBundlesInBasePath(FileConstants.MessageBundleKeys);
        }

        public bool AreMessageFilesLoaded()
        {
            return FileConstants.MessageBundleKeys.All(key => BundleManipulator.IsBundleLoaded(key));
        }

        private bool LoadMessageFiles()
        {
            bool result = BundleManipulator.LoadBundles(FileConstants.MessageBundleKeys);
            if (result)
            {
                foreach (var bundleKey in FileConstants.MessageBundleKeys)
                {
                    var files = BundleManipulator.GetFilesOfBundle(bundleKey);
                    List<MessageFile> bundleMessageFiles = new List<MessageFile>();
                    foreach (var file in files)
                    {
                        if (FileConstants.Bundles[bundleKey].Files.Contains(file.Value["m_Name"].GetValue().AsString()) &&
                            file.Value["labelDataArray"].GetChildrenCount() > 0)
                        {
                            bundleMessageFiles.Add(ConvertToMessageFile(file.Key, file.Value));
                        }
                    }
                    messageFiles.Add(bundleKey, bundleMessageFiles);
                }
            }
            return result;
        }

        private MessageFile ConvertToMessageFile(long pathId, AssetTypeValueField root)
        {
            List<MessageLabel> labels = new List<MessageLabel>();
            foreach (var label in root["labelDataArray"][0].GetChildrenList())
            {
                List<Word> words = new List<Word>();
                foreach (var word in label["wordDataArray"][0].GetChildrenList())
                {
                    string data = word["str"].GetValue().AsString();
                    float width = word["strWidth"].GetValue().AsFloat();
                    words.Add(new Word(data, width));
                }

                string labelName = label["labelName"].GetValue().AsString();
                int labelIndex = label["labelIndex"].GetValue().AsInt();
                int arrayIndex = label["arrayIndex"].GetValue().AsInt();
                labels.Add(new MessageLabel(labelIndex, arrayIndex, labelName, words));
            }

            int languageId = root["langID"].GetValue().AsInt();
            int isKanji = root["isKanji"].GetValue().AsInt();
            string name = root["m_Name"].GetValue().AsString();
            return new MessageFile(labels, languageId, isKanji, name, pathId);
        }

        private List<JObject> ConvertFromMessageFiles(List<MessageFile> messageFiles)
        {
            List<JObject> json = new List<JObject>();

            foreach (MessageFile messageFile in messageFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", messageFile.PathID),
                    new JProperty("ClassID", 0),
                    new JProperty("FileName", messageFile.FileName),
                    new JProperty("langID", messageFile.LanguageID),
                    new JProperty("isKanji", messageFile.IsKanji),
                    new JProperty("labelDataArray",
                        new JArray(
                            from l in messageFile.Labels
                            select new JObject(
                                new JProperty("labelIndex", l.LabelIndex),
                                new JProperty("arrayIndex", l.ArrayIndex),
                                new JProperty("labelName", l.Name),
                                new JProperty("wordDataArray",
                                    new JArray(
                                        from w in l.Words
                                        select new JObject(
                                            new JProperty("str", w.Data),
                                            new JProperty("strWidth", w.Width)
                                        )
                                    )
                                )
                            )
                        )
                    )
                ));
            }

            return json;
        }

        public float CalculateStringLength(string input)
        {
            float total = 0.0f;
            foreach (char character in input)
            {
                if (FileConstants.Characters.Exists(c => c.Character.First() == character))
                {
                    total += FileConstants.Characters.Where(c => c.Character.First() == character).First().Length;
                }
                else
                {
                    total += FileConstants.Characters.Where(c => c.Character.First() == ' ').First().Length;
                }
            }
            return total;
        }
    }
}
