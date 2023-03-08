using BDSPMapInserter.Data;
using BDSPMapInserter.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Data.Utils
{
    internal static class FileConstants
    {
        public static Dictionary<string, BundleInfo> Bundles { get; } = JsonConvert.DeserializeObject<Dictionary<string, BundleInfo>>(Resources.bundle_constants);
        public const string ScriptDataBundleKey = "script-data";
        public const string DprMasterDatasBundleKey = "dpr-masterdatas";
        public const string GameSettingsBundleKey = "gamesettings";
        public const string MessageEnglishBundleKey = "message-en";
        public const string MessageFrenchBundleKey = "message-fr";
        public const string MessageGermanBundleKey = "message-de";
        public const string MessageItalianBundleKey = "message-it";
        public const string MessageJapaneseBundleKey = "message-ja";
        public const string MessageJapaneseKanjiBundleKey = "message-ja-k";
        public const string MessageKoreanBundleKey = "message-ko";
        public const string MessageChineseSimplifiedBundleKey = "message-zh-s";
        public const string MessageSpanishBundleKey = "message-es";
        public const string MessageChineseTraditionalBundleKey = "message-zh-t";

        public static List<string> MessageBundleKeys { get; } = new List<string>() {
            MessageEnglishBundleKey,
            MessageFrenchBundleKey,
            MessageGermanBundleKey,
            MessageItalianBundleKey,
            MessageJapaneseBundleKey,
            MessageJapaneseKanjiBundleKey,
            MessageKoreanBundleKey,
            MessageChineseSimplifiedBundleKey,
            MessageSpanishBundleKey,
            MessageChineseTraditionalBundleKey
        };

        public static List<ZoneInfo> Areas { get; } = JsonConvert.DeserializeObject<List<ZoneInfo>>(Resources.area_id);
        public static List<ZoneInfo> Zones { get; } = JsonConvert.DeserializeObject<List<ZoneInfo>>(Resources.zone_id);

        public static List<CharacterInfo> Characters { get; } = JsonConvert.DeserializeObject<List<CharacterInfo>>(Resources.str_length);
    }
}
