using BDSPMapInserter.Data.Utils;
using BDSPMapInserter.Engine.Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSPMapInserter.Engine.Main
{
    internal class InputValidator
    {
        public List<string> ValidateInput(InputData inputData)
        {
            List<string> validationExceptions = new List<string>();

            if (inputData.AreaID < 0) validationExceptions.Add(string.Format("{0}: {1}", "Area ID", "Value must not be negative."));
            if (inputData.ZoneCode == "") validationExceptions.Add(string.Format("{0}: {1}", "Zone Code", "Value must not be empty."));
            if (inputData.AreaCode == "") validationExceptions.Add(string.Format("{0}: {1}", "Area Code", "Value must not be empty."));
            if (inputData.MapInfoCloneZoneID < 0) validationExceptions.Add(string.Format("{0}: {1}", "MapInfo to Clone", "Value must not be negative."));
            if (inputData.MapWidth < 1) validationExceptions.Add(string.Format("{0}: {1}", "Map Width", "Value must not be less than 1."));
            if (inputData.MapHeight < 1) validationExceptions.Add(string.Format("{0}: {1}", "Map Height", "Value must not be less than 1."));
            if (inputData.AreaName == "") validationExceptions.Add(string.Format("{0}: {1}", "Area Name", "Value must not be empty."));
            if (inputData.AreaNameDisplay == "") validationExceptions.Add(string.Format("{0}: {1}", "Area Name (Display)", "Value must not be empty."));
            if (inputData.AreaNameIndirect == "") validationExceptions.Add(string.Format("{0}: {1}", "Area Name (Indirect)", "Value must not be empty."));
            if (inputData.AreaNameTownMap == "") validationExceptions.Add(string.Format("{0}: {1}", "Area Name (Town Map)", "Value must not be empty."));

            return validationExceptions;
        }
    }
}
