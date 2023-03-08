namespace BDSPMapInserter.UI.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lbZoneIDNum = new System.Windows.Forms.Label();
            this.numAreaID = new System.Windows.Forms.NumericUpDown();
            this.lbAreaID = new System.Windows.Forms.Label();
            this.txtAreaName1 = new System.Windows.Forms.TextBox();
            this.txtAreaName2 = new System.Windows.Forms.TextBox();
            this.txtAreaName3 = new System.Windows.Forms.TextBox();
            this.txtAreaName4 = new System.Windows.Forms.TextBox();
            this.txtZoneCode = new System.Windows.Forms.TextBox();
            this.lbZoneCode = new System.Windows.Forms.Label();
            this.lbAreaName1 = new System.Windows.Forms.Label();
            this.lbAreaName2 = new System.Windows.Forms.Label();
            this.lbAreaName3 = new System.Windows.Forms.Label();
            this.lbAreaName4 = new System.Windows.Forms.Label();
            this.comboMapInfo = new System.Windows.Forms.ComboBox();
            this.lbMapInfo = new System.Windows.Forms.Label();
            this.lbAreaCode = new System.Windows.Forms.Label();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.lbZoneID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAreaID)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::BDSPMapInserter.Properties.Resources.folder;
            this.btnOpen.Location = new System.Drawing.Point(25, 263);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(48, 46);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Enabled = false;
            this.btnExecute.Image = global::BDSPMapInserter.Properties.Resources.save;
            this.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecute.Location = new System.Drawing.Point(79, 263);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(225, 46);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Add new map!";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // lbZoneIDNum
            // 
            this.lbZoneIDNum.Location = new System.Drawing.Point(151, 22);
            this.lbZoneIDNum.Name = "lbZoneIDNum";
            this.lbZoneIDNum.Size = new System.Drawing.Size(64, 19);
            this.lbZoneIDNum.TabIndex = 2;
            this.lbZoneIDNum.Text = "###";
            // 
            // numAreaID
            // 
            this.numAreaID.Enabled = false;
            this.numAreaID.Location = new System.Drawing.Point(151, 44);
            this.numAreaID.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAreaID.Name = "numAreaID";
            this.numAreaID.Size = new System.Drawing.Size(61, 20);
            this.numAreaID.TabIndex = 3;
            // 
            // lbAreaID
            // 
            this.lbAreaID.AutoSize = true;
            this.lbAreaID.Location = new System.Drawing.Point(99, 46);
            this.lbAreaID.Name = "lbAreaID";
            this.lbAreaID.Size = new System.Drawing.Size(46, 13);
            this.lbAreaID.TabIndex = 4;
            this.lbAreaID.Text = "Area ID:";
            // 
            // txtAreaName1
            // 
            this.txtAreaName1.Enabled = false;
            this.txtAreaName1.Location = new System.Drawing.Point(151, 149);
            this.txtAreaName1.Name = "txtAreaName1";
            this.txtAreaName1.Size = new System.Drawing.Size(153, 20);
            this.txtAreaName1.TabIndex = 5;
            // 
            // txtAreaName2
            // 
            this.txtAreaName2.Enabled = false;
            this.txtAreaName2.Location = new System.Drawing.Point(151, 175);
            this.txtAreaName2.Name = "txtAreaName2";
            this.txtAreaName2.Size = new System.Drawing.Size(153, 20);
            this.txtAreaName2.TabIndex = 6;
            // 
            // txtAreaName3
            // 
            this.txtAreaName3.Enabled = false;
            this.txtAreaName3.Location = new System.Drawing.Point(151, 201);
            this.txtAreaName3.Name = "txtAreaName3";
            this.txtAreaName3.Size = new System.Drawing.Size(153, 20);
            this.txtAreaName3.TabIndex = 7;
            // 
            // txtAreaName4
            // 
            this.txtAreaName4.Enabled = false;
            this.txtAreaName4.Location = new System.Drawing.Point(151, 227);
            this.txtAreaName4.Name = "txtAreaName4";
            this.txtAreaName4.Size = new System.Drawing.Size(153, 20);
            this.txtAreaName4.TabIndex = 8;
            // 
            // txtZoneCode
            // 
            this.txtZoneCode.Enabled = false;
            this.txtZoneCode.Location = new System.Drawing.Point(151, 70);
            this.txtZoneCode.Name = "txtZoneCode";
            this.txtZoneCode.Size = new System.Drawing.Size(153, 20);
            this.txtZoneCode.TabIndex = 9;
            // 
            // lbZoneCode
            // 
            this.lbZoneCode.AutoSize = true;
            this.lbZoneCode.Location = new System.Drawing.Point(57, 72);
            this.lbZoneCode.Name = "lbZoneCode";
            this.lbZoneCode.Size = new System.Drawing.Size(88, 13);
            this.lbZoneCode.TabIndex = 10;
            this.lbZoneCode.Text = "New Zone Code:";
            // 
            // lbAreaName1
            // 
            this.lbAreaName1.AutoSize = true;
            this.lbAreaName1.Location = new System.Drawing.Point(82, 152);
            this.lbAreaName1.Name = "lbAreaName1";
            this.lbAreaName1.Size = new System.Drawing.Size(63, 13);
            this.lbAreaName1.TabIndex = 11;
            this.lbAreaName1.Text = "Area Name:";
            // 
            // lbAreaName2
            // 
            this.lbAreaName2.AutoSize = true;
            this.lbAreaName2.Location = new System.Drawing.Point(39, 178);
            this.lbAreaName2.Name = "lbAreaName2";
            this.lbAreaName2.Size = new System.Drawing.Size(106, 13);
            this.lbAreaName2.TabIndex = 12;
            this.lbAreaName2.Text = "Area Name (Display):";
            // 
            // lbAreaName3
            // 
            this.lbAreaName3.AutoSize = true;
            this.lbAreaName3.Location = new System.Drawing.Point(38, 204);
            this.lbAreaName3.Name = "lbAreaName3";
            this.lbAreaName3.Size = new System.Drawing.Size(107, 13);
            this.lbAreaName3.TabIndex = 13;
            this.lbAreaName3.Text = "Area Name (Indirect):";
            // 
            // lbAreaName4
            // 
            this.lbAreaName4.AutoSize = true;
            this.lbAreaName4.Location = new System.Drawing.Point(22, 230);
            this.lbAreaName4.Name = "lbAreaName4";
            this.lbAreaName4.Size = new System.Drawing.Size(123, 13);
            this.lbAreaName4.TabIndex = 14;
            this.lbAreaName4.Text = "Area Name (Town Map):";
            // 
            // comboMapInfo
            // 
            this.comboMapInfo.Enabled = false;
            this.comboMapInfo.FormattingEnabled = true;
            this.comboMapInfo.Location = new System.Drawing.Point(151, 122);
            this.comboMapInfo.Name = "comboMapInfo";
            this.comboMapInfo.Size = new System.Drawing.Size(153, 21);
            this.comboMapInfo.TabIndex = 15;
            // 
            // lbMapInfo
            // 
            this.lbMapInfo.AutoSize = true;
            this.lbMapInfo.Location = new System.Drawing.Point(57, 125);
            this.lbMapInfo.Name = "lbMapInfo";
            this.lbMapInfo.Size = new System.Drawing.Size(88, 13);
            this.lbMapInfo.TabIndex = 16;
            this.lbMapInfo.Text = "Copy MapInfo of:";
            // 
            // lbAreaCode
            // 
            this.lbAreaCode.AutoSize = true;
            this.lbAreaCode.Location = new System.Drawing.Point(60, 99);
            this.lbAreaCode.Name = "lbAreaCode";
            this.lbAreaCode.Size = new System.Drawing.Size(85, 13);
            this.lbAreaCode.TabIndex = 17;
            this.lbAreaCode.Text = "New Area Code:";
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Enabled = false;
            this.txtAreaCode.Location = new System.Drawing.Point(151, 96);
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(153, 20);
            this.txtAreaCode.TabIndex = 18;
            // 
            // ttMain
            // 
            this.ttMain.AutoPopDelay = 50000;
            this.ttMain.InitialDelay = 200;
            this.ttMain.IsBalloon = true;
            this.ttMain.ReshowDelay = 100;
            this.ttMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttMain.ToolTipTitle = "Info";
            // 
            // lbZoneID
            // 
            this.lbZoneID.AutoSize = true;
            this.lbZoneID.Location = new System.Drawing.Point(96, 22);
            this.lbZoneID.Name = "lbZoneID";
            this.lbZoneID.Size = new System.Drawing.Size(49, 13);
            this.lbZoneID.TabIndex = 19;
            this.lbZoneID.Text = "Zone ID:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 324);
            this.Controls.Add(this.txtAreaCode);
            this.Controls.Add(this.lbAreaCode);
            this.Controls.Add(this.lbMapInfo);
            this.Controls.Add(this.comboMapInfo);
            this.Controls.Add(this.lbZoneCode);
            this.Controls.Add(this.txtZoneCode);
            this.Controls.Add(this.lbAreaName4);
            this.Controls.Add(this.lbAreaName3);
            this.Controls.Add(this.lbAreaName2);
            this.Controls.Add(this.lbAreaName1);
            this.Controls.Add(this.txtAreaName4);
            this.Controls.Add(this.txtAreaName3);
            this.Controls.Add(this.txtAreaName2);
            this.Controls.Add(this.txtAreaName1);
            this.Controls.Add(this.lbAreaID);
            this.Controls.Add(this.numAreaID);
            this.Controls.Add(this.lbZoneID);
            this.Controls.Add(this.lbZoneIDNum);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(338, 363);
            this.MinimumSize = new System.Drawing.Size(338, 363);
            this.Name = "FormMain";
            this.Text = "BDSP Map Inserter";
            ((System.ComponentModel.ISupportInitialize)(this.numAreaID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label lbZoneIDNum;
        private System.Windows.Forms.NumericUpDown numAreaID;
        private System.Windows.Forms.Label lbAreaID;
        private System.Windows.Forms.TextBox txtAreaName1;
        private System.Windows.Forms.TextBox txtAreaName2;
        private System.Windows.Forms.TextBox txtAreaName3;
        private System.Windows.Forms.TextBox txtAreaName4;
        private System.Windows.Forms.TextBox txtZoneCode;
        private System.Windows.Forms.Label lbZoneCode;
        private System.Windows.Forms.Label lbAreaName1;
        private System.Windows.Forms.Label lbAreaName2;
        private System.Windows.Forms.Label lbAreaName3;
        private System.Windows.Forms.Label lbAreaName4;
        private System.Windows.Forms.ComboBox comboMapInfo;
        private System.Windows.Forms.Label lbMapInfo;
        private System.Windows.Forms.Label lbAreaCode;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.Label lbZoneID;
    }
}

