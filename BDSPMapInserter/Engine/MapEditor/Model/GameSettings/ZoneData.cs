using BDSPMapInserter.Engine.MessageEditor.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class ZoneData
    {
        public string Caption { get; set; }
        public string MSLabel { get; set; }
        public string PokePlaceName { get; set; }
        public string FlyingPlaceName { get; set; }
        public int MapType { get; set; }
        public int IsField { get; set; }
        public int LandmarkType { get; set; }
        public PointF MiniMapOffset { get; set; }
        public int Bicycle { get; set; }
        public int Escape { get; set; }
        public int Fly { get; set; }
        public int BattleSearcher { get; set; }
        public int TureAruki { get; set; }
        public int KuruKuru { get; set; }
        public int Fall { get; set; }
        public List<int> BattleBg { get; set; }
        public int ZoneID { get; set; }
        public int AreaID { get; set; }
        public long ZoneGridPathID { get; set; }
        public long AttributePathID { get; set; }
        public long AttributeExPathID { get; set; }
        public long SubAttributePathID { get; set; }
        public long SubAttributeExPathID { get; set; }
        public List<string> BGM { get; set; }
        public string EnvironmentalSound { get; set; }
        public int Weather { get; set; }
        public long RenderSettingsPathID { get; set; }
        public int ReflectionCamera { get; set; }
        public int FixedTime { get; set; }
        public string AssetBundleName { get; set; }
        public int RoomPanCamera { get; set; }
        public List<Locator> Locators { get; set; }
        public int Reload { get; set; }

        internal struct Locator
        {
            float x;
            float y;
            float z;
            float w;

            public Locator(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            public float GetX() { return x; }
            public float GetY() { return y; }
            public float GetZ() { return z; }
            public float GetW() { return w; }
        }

        public ZoneData(){}

        public ZoneData(ZoneData zoneData)
        {
            Caption = zoneData.Caption;
            MSLabel = zoneData.MSLabel;
            PokePlaceName = zoneData.PokePlaceName;
            FlyingPlaceName = zoneData.FlyingPlaceName;
            MapType = zoneData.MapType;
            IsField = zoneData.IsField;
            LandmarkType = zoneData.LandmarkType;
            MiniMapOffset = new PointF
            (
                zoneData.MiniMapOffset.X,
                zoneData.MiniMapOffset.Y
            );
            Bicycle = zoneData.Bicycle;
            Escape = zoneData.Escape;
            Fly = zoneData.Fly;
            BattleSearcher = zoneData.BattleSearcher;
            TureAruki = zoneData.TureAruki;
            KuruKuru = zoneData.KuruKuru;
            Fall = zoneData.Fall;
            BattleBg = new List<int>(zoneData.BattleBg);
            ZoneID = zoneData.ZoneID;
            AreaID = zoneData.AreaID;
            ZoneGridPathID = zoneData.ZoneGridPathID;
            AttributePathID = zoneData.AttributePathID;
            AttributeExPathID = zoneData.AttributeExPathID;
            SubAttributePathID = zoneData.SubAttributePathID;
            SubAttributeExPathID = zoneData.SubAttributeExPathID;
            BGM = new List<string>(zoneData.BGM);
            EnvironmentalSound = zoneData.EnvironmentalSound;
            Weather = zoneData.Weather;
            RenderSettingsPathID = zoneData.RenderSettingsPathID;
            ReflectionCamera = zoneData.ReflectionCamera;
            FixedTime = zoneData.FixedTime;
            AssetBundleName = zoneData.AssetBundleName;
            RoomPanCamera = zoneData.RoomPanCamera;
            Locators = new List<Locator>(zoneData.Locators);
            Reload = zoneData.Reload;
        }
    }
}
