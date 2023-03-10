using AssetsTools.NET;
using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDSPMapInserter.Engine.ScriptEditor.Model;

namespace BDSPMapInserter.Engine.ScriptEditor
{
    internal class ScriptEditorEngine
    {
        private List<ScriptFile> scriptFiles;

        public ScriptEditorEngine()
        {
            scriptFiles = new List<ScriptFile>();
        }

        public List<ScriptFile> GetScriptFiles()
        {
            if (!AreScriptFilesLoaded()) LoadScriptFiles();
            return scriptFiles;
        }

        public void SetScriptFiles(List<ScriptFile> scriptFiles)
        {
            List<ScriptFile> newFiles = new List<ScriptFile>();
            foreach (ScriptFile file in scriptFiles)
            {
                if (!this.scriptFiles.Exists(f => f.PathID == file.PathID))
                {
                    newFiles.Add(file);
                    string container = string.Format("assets/evscriptdata/eventasset/{0}.asset", file.FileName);
                    BundleManipulator.AddNewFileToBundle(FileConstants.ScriptDataBundleKey, ConvertFromScriptFiles(new List<ScriptFile>() { file })[0], 114, 0xffff, container);
                }
            }
            scriptFiles.RemoveAll(f => newFiles.Contains(f));
            BundleManipulator.SetFilesToBundle(FileConstants.ScriptDataBundleKey, ConvertFromScriptFiles(scriptFiles));
        }

        public bool SaveScriptFiles(string path)
        {
            return BundleManipulator.SaveBundles(new List<string>() { FileConstants.ScriptDataBundleKey }, path);
        }

        public bool SaveScriptFilesInBasePath()
        {
            return BundleManipulator.SaveBundlesInBasePath(new List<string>() { FileConstants.ScriptDataBundleKey });
        }

        public bool AreScriptFilesLoaded()
        {
            return BundleManipulator.IsBundleLoaded(FileConstants.ScriptDataBundleKey);
        }

        private bool LoadScriptFiles()
        {
            bool result = BundleManipulator.LoadBundles(new List<string>() { FileConstants.ScriptDataBundleKey });
            if (result)
            {
                var files = BundleManipulator.GetFilesOfBundle(FileConstants.ScriptDataBundleKey);
                foreach (var file in files)
                {
                    if (FileConstants.Bundles[FileConstants.ScriptDataBundleKey].Files.Contains(file.Value["m_Name"].GetValue().AsString()) && 
                        file.Value["Scripts"].GetChildrenCount() > 0)
                    {
                        scriptFiles.Add(ConvertToScriptFile(file.Key, file.Value));
                    }
                }
            }
            return result;
        }

        private ScriptFile ConvertToScriptFile(long pathId, AssetTypeValueField root)
        {
            List<string> strings = new List<string>();
            foreach (var str in root["StrList"][0].GetChildrenList())
            {
                strings.Add(str.GetValue().AsString());
            }

            List<Script> scripts = new List<Script>();
            foreach (var script in root["Scripts"][0].GetChildrenList())
            {
                List<Command> commands = new List<Command>();
                foreach (var command in script["Commands"][0].GetChildrenList())
                {
                    List<Argument> args = new List<Argument>();
                    foreach (var arg in command["Arg"][0].GetChildrenList())
                    {
                        ArgumentType type = (ArgumentType)arg["argType"].GetValue().AsInt();
                        if (type == ArgumentType.String) args.Add(new Argument(type, strings[arg["data"].GetValue().AsInt()]));
                        else args.Add(new Argument(type, arg["data"].GetValue().AsInt()));
                    }
                    commands.Add(new Command(args));
                }
                scripts.Add(new Script(script["Label"].GetValue().AsString(), commands));
            }

            return new ScriptFile(strings, scripts, root["m_Name"].GetValue().AsString(), pathId);
        }

        private List<JObject> ConvertFromScriptFiles(List<ScriptFile> scriptFiles)
        {
            List<ScriptFile> convertedScriptFiles = ConvertStringsToIndex(scriptFiles);

            List<JObject> json = new List<JObject>();

            foreach (ScriptFile scriptFile in convertedScriptFiles)
            {
                json.Add(new JObject(
                    new JProperty("PathID", scriptFile.PathID),
                    new JProperty("ClassID", 0),
                    new JProperty("FileName", scriptFile.FileName),
                    new JProperty("Scripts",
                        new JArray(
                            from s in scriptFile.Scripts
                            select new JObject(
                                new JProperty("Label", new JValue(s.Name)),
                                new JProperty("Commands",
                                    new JArray(
                                        from c in s.Commands
                                        select new JObject(
                                            new JProperty("Arg",
                                                new JArray(
                                                    from a in c.Arguments
                                                    select new JObject(
                                                        new JProperty("argType", a.Type),
                                                        new JProperty("data", a.GetNumberValue())
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    new JProperty("StrList",
                        new JArray(
                            from s in scriptFile.Strings
                            select new JValue(s)
                        )
                    )
                ));
            }

            return json;
        }

        private List<ScriptFile> ConvertStringsToIndex(List<ScriptFile> scriptFiles)
        {
            foreach (ScriptFile scriptFile in scriptFiles)
            {
                List<string> strings = new List<string>();

                foreach (Script script in scriptFile.Scripts)
                {
                    foreach (Command command in script.Commands)
                    {
                        foreach (Argument arg in command.Arguments)
                        {
                            if (arg.Type == ArgumentType.String)
                            {
                                if (strings.Contains(arg.GetStringValue()))
                                {
                                    arg.SetNumberValue(strings.IndexOf(arg.GetStringValue()));
                                }
                                else
                                {
                                    arg.SetNumberValue(strings.Count);
                                    strings.Add(arg.GetStringValue());
                                }
                            }
                        }
                    }
                }

                scriptFile.Strings = strings;
            }

            return scriptFiles;
        }
    }
}
