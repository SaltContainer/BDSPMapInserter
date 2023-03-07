using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.MapEditor.Model
{
    internal class Camera
    {
        public int AreaID { get; set; }
        public float Pitch { get; set; }
        public float FOV { get; set; }
        public float TargetRange { get; set; }
        public float PanDistance { get; set; }
        public float PanPitch { get; set; }
        public float PanFOV { get; set; }
        public int PanPosUseFlag { get; set; }
        public float PanMinPosY { get; set; }
        public float PanMaxPosY { get; set; }
        public float PanMinPosZ { get; set; }
        public float PanMaxPosZ { get; set; }
        public float DefocusStart { get; set; }
        public float DefocusEnd { get; set; }
        public float Distance { get; set; }
    }
}
