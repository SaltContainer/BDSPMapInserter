using BDSPMapInserter.Engine;
using BDSPMapInserter.Properties;
using BDSPMapInserter.UI.Model;
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
            if (txtZoneCode.Text == "") MessageBox.Show("The Zone Code is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAreaCode.Text == "") MessageBox.Show("The Area Code is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAreaName1.Text == "") MessageBox.Show("The Area Name is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAreaName2.Text == "") MessageBox.Show("The Area Name (Display) is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAreaName3.Text == "") MessageBox.Show("The Area Name (Indirect) is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAreaName4.Text == "") MessageBox.Show("The Area Name (Town Map) is empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                ClonableMapInfoData clonableMapInfo = (ClonableMapInfoData)comboMapInfo.SelectedItem;
                engine.InsertNewMapInfo(clonableMapInfo.ZoneID, engine.GetNextZoneID(), (int)numAreaID.Value, 3827560303091868358, -5767685015742308502, -2815371549301195827);
                MessageBox.Show("Not yet implemented.", "WIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
