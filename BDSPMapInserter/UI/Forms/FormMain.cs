using BDSPMapInserter.Engine.Main;
using BDSPMapInserter.Engine.Main.Model;
using BDSPMapInserter.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDSPMapInserter.UI.Forms
{
    public partial class FormMain : Form
    {
        private MainEngine engine;

        public FormMain()
        {
            InitializeComponent();
            Icon = Resources.sinnoh_ico;
            SetTooltips();

            engine = new MainEngine();
        }

        private void SetTooltips()
        {
            ttMain.SetToolTip(lbZoneID, "The Zone ID that will be given to the new map.\nThis has to be sequential and cannot be changed.");
            ttMain.SetToolTip(lbZoneIDNum, "The Zone ID that will be given to the new map.\nThis has to be sequential and cannot be changed.");
            ttMain.SetToolTip(lbAreaID, "The Area ID that will be given to the new map.\nThe default value is the next id after the highest used Area ID.");
            ttMain.SetToolTip(numAreaID, "The Area ID that will be given to the new map.\nThe default value is the next id after the highest used Area ID.");
            ttMain.SetToolTip(lbZoneCode, "The \"Zone Code\" that will be given to the new map.\nThis will be used for the names of the script files.");
            ttMain.SetToolTip(txtZoneCode, "The \"Zone Code\" that will be given to the new map.\nThis will be used for the names of the script files.");
            ttMain.SetToolTip(lbAreaCode, "The \"Area Code\" that will be given to the new map.\nThis will be used for the names of mapwarp, placedata, and stopdata files.");
            ttMain.SetToolTip(txtAreaCode, "The \"Area Code\" that will be given to the new map.\nThis will be used for the names of mapwarp, placedata, and stopdata files.");
            ttMain.SetToolTip(lbMapInfo, "The map from which to copy the MapInfo data from.\nCopies both the ZoneData and the Camera data.");
            ttMain.SetToolTip(comboMapInfo, "The map from which to copy the MapInfo data from.\nCopies both the ZoneData and the Camera data.");
            ttMain.SetToolTip(lbSinnoh, "Check if you plan to use this map in the Sinnoh Matrix.\nThis skips the creation of Attribute Matrix files.");
            ttMain.SetToolTip(checkSinnoh, "Check if you plan to use this map in the Sinnoh Matrix.\nThis skips the creation of Attribute Matrix files.");
            ttMain.SetToolTip(lbMapSize, "The size of the new map, in 32-tile chunks.");
            ttMain.SetToolTip(lbMapWidth, "The width of the new map, in 32-tile chunks.");
            ttMain.SetToolTip(numMapWidth, "The width of the new map, in 32-tile chunks.");
            ttMain.SetToolTip(lbMapHeight, "The height of the new map, in 32-tile chunks.");
            ttMain.SetToolTip(numMapHeight, "The height of the new map, in 32-tile chunks.");
            ttMain.SetToolTip(lbAreaName1, "The user-friendly name of the area.");
            ttMain.SetToolTip(txtAreaName1, "The user-friendly name of the area.");
            ttMain.SetToolTip(lbAreaName2, "The user-friendly name of the area (Displayed when entering?).");
            ttMain.SetToolTip(txtAreaName2, "The user-friendly name of the area (Displayed when entering?).");
            ttMain.SetToolTip(lbAreaName3, "The user-friendly name of the area (Indirect).\nEx: \"in Jubilife City\"");
            ttMain.SetToolTip(txtAreaName3, "The user-friendly name of the area (Indirect).\nEx: \"in Jubilife City\"");
            ttMain.SetToolTip(lbAreaName4, "The user-friendly name of the area (Shown in the Town Map).");
            ttMain.SetToolTip(txtAreaName4, "The user-friendly name of the area (Shown in the Town Map).");
            ttMain.SetToolTip(btnOpen, "Open the romfs folder containing the files to edit.");
            ttMain.SetToolTip(btnExecute, "Add all the necessary files to add a map.");
        }

        private void EnableControlsOnOpen()
        {
            btnOpen.Enabled = false;
            btnExecute.Enabled = true;
            numAreaID.Enabled = true;
            txtZoneCode.Enabled = true;
            txtAreaCode.Enabled = true;
            comboMapInfo.Enabled = true;
            checkSinnoh.Enabled = true;
            numMapWidth.Enabled = true;
            numMapHeight.Enabled = true;
            txtAreaName1.Enabled = true;
            txtAreaName2.Enabled = true;
            txtAreaName3.Enabled = true;
            txtAreaName4.Enabled = true;
        }

        private void InitialValues()
        {
            lbZoneIDNum.Text = string.Format("{0}", engine.GetNextZoneID());
            numAreaID.Value = engine.GetNextAreaID();
            comboMapInfo.DataSource = engine.GetClonableMapInfoData();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (engine.SetBasePath(dialog.FileName))
                {
                    InitialValues();
                    EnableControlsOnOpen();
                }
                else
                {
                    MessageBox.Show(string.Format("\"{0}\" does not contain the \"{1}\" folder, or it could not be read.", dialog.FileName, "Data"), "Invalid folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ClonableMapInfoData clonableMapInfo = (ClonableMapInfoData)comboMapInfo.SelectedItem;
            InputData inputData = new InputData()
            {
                ZoneID = engine.GetNextZoneID(),
                AreaID = (int)numAreaID.Value,
                ZoneCode = txtZoneCode.Text,
                AreaCode = txtAreaCode.Text,
                MapInfoCloneZoneID = clonableMapInfo.ZoneID,
                IsSinnoh = checkSinnoh.Checked,
                MapWidth = (int)numMapWidth.Value,
                MapHeight = (int)numMapHeight.Value,
                AreaName = txtAreaName1.Text,
                AreaNameDisplay = txtAreaName2.Text,
                AreaNameIndirect = txtAreaName3.Text,
                AreaNameTownMap = txtAreaName4.Text
            };
            List<string> errors = engine.ValidateInput(inputData);
            if (errors.Count > 0)
            {
                string fullError = string.Join("\n", errors);
                MessageBox.Show(fullError, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                engine.InsertNewMapInfo(inputData);
                MessageBox.Show("Not yet implemented.", "WIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
