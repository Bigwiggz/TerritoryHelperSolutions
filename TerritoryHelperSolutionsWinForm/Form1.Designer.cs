namespace TerritoryHelperSolutionsWinForm
{
    partial class panelSideMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(panelSideMenu));
            panel1 = new Panel();
            label3 = new Label();
            btnHelp = new FontAwesome.Sharp.IconButton();
            btnUpdateCensusTerritories = new FontAwesome.Sharp.IconButton();
            btnSearchAToZDB = new FontAwesome.Sharp.IconButton();
            btnSaveTerritoryInformation = new FontAwesome.Sharp.IconButton();
            btnGetTerritoryInformation = new FontAwesome.Sharp.IconButton();
            btnConfiguration = new FontAwesome.Sharp.IconButton();
            btnInstructions = new FontAwesome.Sharp.IconButton();
            panelLogo = new Panel();
            btnHome1 = new Label();
            btnHome2 = new Label();
            panelBottom = new Panel();
            label1 = new Label();
            panelDesktop = new Panel();
            label4 = new Label();
            label2 = new Label();
            panelTitleBar = new Panel();
            btnOutputFolder = new FontAwesome.Sharp.IconButton();
            lblMinimize = new Label();
            lblMaximize = new Label();
            lblExit = new Label();
            lblTitleChildForm = new Label();
            iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            iconSplitButton1 = new FontAwesome.Sharp.IconSplitButton();
            openFileDialogOutput = new OpenFileDialog();
            btnAddressErrorScanner = new FontAwesome.Sharp.IconButton();
            panel1.SuspendLayout();
            panelLogo.SuspendLayout();
            panelBottom.SuspendLayout();
            panelDesktop.SuspendLayout();
            panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconCurrentChildForm).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(11, 7, 17);
            panel1.Controls.Add(btnAddressErrorScanner);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnHelp);
            panel1.Controls.Add(btnUpdateCensusTerritories);
            panel1.Controls.Add(btnSearchAToZDB);
            panel1.Controls.Add(btnSaveTerritoryInformation);
            panel1.Controls.Add(btnGetTerritoryInformation);
            panel1.Controls.Add(btnConfiguration);
            panel1.Controls.Add(btnInstructions);
            panel1.Controls.Add(panelLogo);
            panel1.Dock = DockStyle.Left;
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 561);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 254);
            label3.Name = "label3";
            label3.Size = new Size(95, 37);
            label3.TabIndex = 9;
            label3.Text = "Scripts";
            // 
            // btnHelp
            // 
            btnHelp.Dock = DockStyle.Top;
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnHelp.IconChar = FontAwesome.Sharp.IconChar.QuestionCircle;
            btnHelp.IconColor = Color.White;
            btnHelp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHelp.IconSize = 40;
            btnHelp.ImageAlign = ContentAlignment.MiddleLeft;
            btnHelp.Location = new Point(0, 190);
            btnHelp.Name = "btnHelp";
            btnHelp.Padding = new Padding(10, 0, 20, 0);
            btnHelp.Size = new Size(250, 45);
            btnHelp.TabIndex = 8;
            btnHelp.Text = "Help";
            btnHelp.TextAlign = ContentAlignment.MiddleLeft;
            btnHelp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnUpdateCensusTerritories
            // 
            btnUpdateCensusTerritories.FlatAppearance.BorderSize = 0;
            btnUpdateCensusTerritories.FlatStyle = FlatStyle.Flat;
            btnUpdateCensusTerritories.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdateCensusTerritories.IconChar = FontAwesome.Sharp.IconChar.LocationDot;
            btnUpdateCensusTerritories.IconColor = Color.White;
            btnUpdateCensusTerritories.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUpdateCensusTerritories.IconSize = 40;
            btnUpdateCensusTerritories.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdateCensusTerritories.Location = new Point(0, 489);
            btnUpdateCensusTerritories.Name = "btnUpdateCensusTerritories";
            btnUpdateCensusTerritories.Padding = new Padding(10, 0, 20, 0);
            btnUpdateCensusTerritories.Size = new Size(250, 45);
            btnUpdateCensusTerritories.TabIndex = 7;
            btnUpdateCensusTerritories.Text = "Update Census Territories";
            btnUpdateCensusTerritories.TextAlign = ContentAlignment.MiddleLeft;
            btnUpdateCensusTerritories.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUpdateCensusTerritories.UseVisualStyleBackColor = true;
            btnUpdateCensusTerritories.Click += btnUpdateCensusTerritories_Click;
            // 
            // btnSearchAToZDB
            // 
            btnSearchAToZDB.FlatAppearance.BorderSize = 0;
            btnSearchAToZDB.FlatStyle = FlatStyle.Flat;
            btnSearchAToZDB.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSearchAToZDB.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            btnSearchAToZDB.IconColor = Color.White;
            btnSearchAToZDB.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSearchAToZDB.IconSize = 40;
            btnSearchAToZDB.ImageAlign = ContentAlignment.MiddleLeft;
            btnSearchAToZDB.Location = new Point(0, 444);
            btnSearchAToZDB.Name = "btnSearchAToZDB";
            btnSearchAToZDB.Padding = new Padding(10, 0, 20, 0);
            btnSearchAToZDB.Size = new Size(250, 45);
            btnSearchAToZDB.TabIndex = 6;
            btnSearchAToZDB.Text = "Search A to Z Db";
            btnSearchAToZDB.TextAlign = ContentAlignment.MiddleLeft;
            btnSearchAToZDB.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSearchAToZDB.UseVisualStyleBackColor = true;
            btnSearchAToZDB.Click += btnSearchAToZDB_Click;
            // 
            // btnSaveTerritoryInformation
            // 
            btnSaveTerritoryInformation.FlatAppearance.BorderSize = 0;
            btnSaveTerritoryInformation.FlatStyle = FlatStyle.Flat;
            btnSaveTerritoryInformation.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveTerritoryInformation.IconChar = FontAwesome.Sharp.IconChar.Upload;
            btnSaveTerritoryInformation.IconColor = Color.White;
            btnSaveTerritoryInformation.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSaveTerritoryInformation.IconSize = 40;
            btnSaveTerritoryInformation.ImageAlign = ContentAlignment.MiddleLeft;
            btnSaveTerritoryInformation.Location = new Point(0, 399);
            btnSaveTerritoryInformation.Name = "btnSaveTerritoryInformation";
            btnSaveTerritoryInformation.Padding = new Padding(10, 0, 20, 0);
            btnSaveTerritoryInformation.Size = new Size(250, 45);
            btnSaveTerritoryInformation.TabIndex = 5;
            btnSaveTerritoryInformation.Text = "Save Territory Information";
            btnSaveTerritoryInformation.TextAlign = ContentAlignment.MiddleLeft;
            btnSaveTerritoryInformation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveTerritoryInformation.UseVisualStyleBackColor = true;
            btnSaveTerritoryInformation.Click += btnSaveTerritoryInformation_Click;
            // 
            // btnGetTerritoryInformation
            // 
            btnGetTerritoryInformation.FlatAppearance.BorderSize = 0;
            btnGetTerritoryInformation.FlatStyle = FlatStyle.Flat;
            btnGetTerritoryInformation.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnGetTerritoryInformation.IconChar = FontAwesome.Sharp.IconChar.Download;
            btnGetTerritoryInformation.IconColor = Color.White;
            btnGetTerritoryInformation.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGetTerritoryInformation.IconSize = 40;
            btnGetTerritoryInformation.ImageAlign = ContentAlignment.MiddleLeft;
            btnGetTerritoryInformation.Location = new Point(0, 354);
            btnGetTerritoryInformation.Name = "btnGetTerritoryInformation";
            btnGetTerritoryInformation.Padding = new Padding(10, 0, 20, 0);
            btnGetTerritoryInformation.Size = new Size(250, 45);
            btnGetTerritoryInformation.TabIndex = 4;
            btnGetTerritoryInformation.Text = "Get Territory Information";
            btnGetTerritoryInformation.TextAlign = ContentAlignment.MiddleLeft;
            btnGetTerritoryInformation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGetTerritoryInformation.UseVisualStyleBackColor = true;
            btnGetTerritoryInformation.Click += btnGetTerritoryInformation_Click;
            // 
            // btnConfiguration
            // 
            btnConfiguration.Dock = DockStyle.Top;
            btnConfiguration.FlatAppearance.BorderSize = 0;
            btnConfiguration.FlatStyle = FlatStyle.Flat;
            btnConfiguration.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnConfiguration.IconChar = FontAwesome.Sharp.IconChar.Gear;
            btnConfiguration.IconColor = Color.White;
            btnConfiguration.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnConfiguration.IconSize = 40;
            btnConfiguration.ImageAlign = ContentAlignment.MiddleLeft;
            btnConfiguration.Location = new Point(0, 145);
            btnConfiguration.Name = "btnConfiguration";
            btnConfiguration.Padding = new Padding(10, 0, 20, 0);
            btnConfiguration.Size = new Size(250, 45);
            btnConfiguration.TabIndex = 3;
            btnConfiguration.Text = "Configuration";
            btnConfiguration.TextAlign = ContentAlignment.MiddleLeft;
            btnConfiguration.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnConfiguration.UseVisualStyleBackColor = true;
            btnConfiguration.Click += btnConfiguration_Click;
            // 
            // btnInstructions
            // 
            btnInstructions.Dock = DockStyle.Top;
            btnInstructions.FlatAppearance.BorderSize = 0;
            btnInstructions.FlatStyle = FlatStyle.Flat;
            btnInstructions.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnInstructions.IconChar = FontAwesome.Sharp.IconChar.BookOpen;
            btnInstructions.IconColor = Color.White;
            btnInstructions.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInstructions.IconSize = 40;
            btnInstructions.ImageAlign = ContentAlignment.MiddleLeft;
            btnInstructions.Location = new Point(0, 100);
            btnInstructions.Name = "btnInstructions";
            btnInstructions.Padding = new Padding(10, 0, 20, 0);
            btnInstructions.Size = new Size(250, 45);
            btnInstructions.TabIndex = 2;
            btnInstructions.Text = "Instructions";
            btnInstructions.TextAlign = ContentAlignment.MiddleLeft;
            btnInstructions.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnInstructions.UseVisualStyleBackColor = true;
            btnInstructions.Click += btnInstructions_Click;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(btnHome1);
            panelLogo.Controls.Add(btnHome2);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(250, 100);
            panelLogo.TabIndex = 0;
            // 
            // btnHome1
            // 
            btnHome1.Anchor = AnchorStyles.None;
            btnHome1.AutoSize = true;
            btnHome1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnHome1.Location = new Point(12, 43);
            btnHome1.Name = "btnHome1";
            btnHome1.Size = new Size(220, 25);
            btnHome1.TabIndex = 1;
            btnHome1.Text = "Territory Helper Scripts";
            btnHome1.Click += btnHome1_Click;
            // 
            // btnHome2
            // 
            btnHome2.AutoSize = true;
            btnHome2.Font = new Font("Segoe UI", 54.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnHome2.Location = new Point(31, 0);
            btnHome2.Name = "btnHome2";
            btnHome2.Size = new Size(182, 98);
            btnHome2.TabIndex = 0;
            btnHome2.Text = "THS";
            btnHome2.Click += btnHome2_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(23, 21, 32);
            panelBottom.Controls.Add(label1);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(250, 521);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(684, 40);
            panelBottom.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(588, 16);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 3;
            label1.Text = "Version:  1.0.0";
            // 
            // panelDesktop
            // 
            panelDesktop.BackColor = Color.FromArgb(32, 30, 45);
            panelDesktop.Controls.Add(label4);
            panelDesktop.Controls.Add(label2);
            panelDesktop.Controls.Add(panelTitleBar);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            panelDesktop.Location = new Point(250, 0);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(684, 521);
            panelDesktop.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Coral;
            label4.Location = new Point(42, 159);
            label4.MaximumSize = new Size(600, 0);
            label4.Name = "label4";
            label4.Size = new Size(587, 38);
            label4.TabIndex = 53;
            label4.Text = "This is an application to run Territory Helper Scripts.  Please run review the instructions section before running the scripts below.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Coral;
            label2.Location = new Point(200, 100);
            label2.Name = "label2";
            label2.Size = new Size(332, 25);
            label2.TabIndex = 3;
            label2.Text = "Welcome To Territory Helper Scripts";
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.FromArgb(23, 21, 32);
            panelTitleBar.Controls.Add(btnOutputFolder);
            panelTitleBar.Controls.Add(lblMinimize);
            panelTitleBar.Controls.Add(lblMaximize);
            panelTitleBar.Controls.Add(lblExit);
            panelTitleBar.Controls.Add(lblTitleChildForm);
            panelTitleBar.Controls.Add(iconCurrentChildForm);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(0, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(684, 40);
            panelTitleBar.TabIndex = 2;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            // 
            // btnOutputFolder
            // 
            btnOutputFolder.FlatStyle = FlatStyle.Flat;
            btnOutputFolder.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnOutputFolder.ForeColor = Color.Coral;
            btnOutputFolder.IconChar = FontAwesome.Sharp.IconChar.Folder;
            btnOutputFolder.IconColor = Color.Coral;
            btnOutputFolder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOutputFolder.IconSize = 18;
            btnOutputFolder.Location = new Point(269, 4);
            btnOutputFolder.Name = "btnOutputFolder";
            btnOutputFolder.Size = new Size(192, 30);
            btnOutputFolder.TabIndex = 5;
            btnOutputFolder.Text = "Output Folder";
            btnOutputFolder.TextAlign = ContentAlignment.MiddleLeft;
            btnOutputFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOutputFolder.UseVisualStyleBackColor = true;
            btnOutputFolder.Click += btnOutputFolder_Click;
            // 
            // lblMinimize
            // 
            lblMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMinimize.AutoSize = true;
            lblMinimize.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblMinimize.ForeColor = Color.White;
            lblMinimize.Location = new Point(597, 7);
            lblMinimize.Name = "lblMinimize";
            lblMinimize.Size = new Size(20, 25);
            lblMinimize.TabIndex = 4;
            lblMinimize.Text = "_";
            lblMinimize.Click += lblMinimize_Click;
            // 
            // lblMaximize
            // 
            lblMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMaximize.AutoSize = true;
            lblMaximize.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblMaximize.ForeColor = Color.White;
            lblMaximize.Location = new Point(627, 7);
            lblMaximize.Name = "lblMaximize";
            lblMaximize.Size = new Size(29, 25);
            lblMaximize.TabIndex = 3;
            lblMaximize.Text = "▢";
            lblMaximize.Click += lblMaximize_Click;
            // 
            // lblExit
            // 
            lblExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblExit.AutoSize = true;
            lblExit.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblExit.ForeColor = Color.White;
            lblExit.Location = new Point(657, 7);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(24, 25);
            lblExit.TabIndex = 2;
            lblExit.Text = "X";
            lblExit.Click += lblExit_Click;
            // 
            // lblTitleChildForm
            // 
            lblTitleChildForm.AutoSize = true;
            lblTitleChildForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitleChildForm.ForeColor = Color.White;
            lblTitleChildForm.Location = new Point(42, 8);
            lblTitleChildForm.Name = "lblTitleChildForm";
            lblTitleChildForm.Size = new Size(56, 21);
            lblTitleChildForm.TabIndex = 1;
            lblTitleChildForm.Text = "Home";
            // 
            // iconCurrentChildForm
            // 
            iconCurrentChildForm.BackColor = Color.FromArgb(23, 21, 32);
            iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.Home;
            iconCurrentChildForm.IconColor = Color.White;
            iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconCurrentChildForm.Location = new Point(5, 4);
            iconCurrentChildForm.Name = "iconCurrentChildForm";
            iconCurrentChildForm.Size = new Size(32, 32);
            iconCurrentChildForm.TabIndex = 0;
            iconCurrentChildForm.TabStop = false;
            // 
            // iconSplitButton1
            // 
            iconSplitButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            iconSplitButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconSplitButton1.IconColor = Color.Black;
            iconSplitButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconSplitButton1.IconSize = 48;
            iconSplitButton1.Name = "iconSplitButton1";
            iconSplitButton1.Rotation = 0D;
            iconSplitButton1.Size = new Size(23, 23);
            iconSplitButton1.Text = "iconSplitButton1";
            // 
            // openFileDialogOutput
            // 
            openFileDialogOutput.FileName = "openFileDialog1";
            // 
            // btnAddressErrorScanner
            // 
            btnAddressErrorScanner.FlatAppearance.BorderSize = 0;
            btnAddressErrorScanner.FlatStyle = FlatStyle.Flat;
            btnAddressErrorScanner.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddressErrorScanner.IconChar = FontAwesome.Sharp.IconChar.Satellite;
            btnAddressErrorScanner.IconColor = Color.White;
            btnAddressErrorScanner.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddressErrorScanner.IconSize = 40;
            btnAddressErrorScanner.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddressErrorScanner.Location = new Point(0, 303);
            btnAddressErrorScanner.Name = "btnAddressErrorScanner";
            btnAddressErrorScanner.Padding = new Padding(10, 0, 20, 0);
            btnAddressErrorScanner.Size = new Size(250, 45);
            btnAddressErrorScanner.TabIndex = 10;
            btnAddressErrorScanner.Text = "Address Error Scanner";
            btnAddressErrorScanner.TextAlign = ContentAlignment.MiddleLeft;
            btnAddressErrorScanner.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddressErrorScanner.UseVisualStyleBackColor = true;
            btnAddressErrorScanner.Click += btnAddressErrorScanner_Click;
            // 
            // panelSideMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 561);
            Controls.Add(panelDesktop);
            Controls.Add(panelBottom);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(950, 600);
            Name = "panelSideMenu";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            panelDesktop.ResumeLayout(false);
            panelDesktop.PerformLayout();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconCurrentChildForm).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panelLogo;
        private Panel panelBottom;
        private Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnInstructions;
        private FontAwesome.Sharp.IconButton btnHelp;
        private FontAwesome.Sharp.IconButton btnUpdateCensusTerritories;
        private FontAwesome.Sharp.IconButton btnSearchAToZDB;
        private FontAwesome.Sharp.IconButton btnSaveTerritoryInformation;
        private FontAwesome.Sharp.IconButton btnGetTerritoryInformation;
        private FontAwesome.Sharp.IconButton btnConfiguration;
        private Label btnHome2;
        private Label btnHome1;
        private Panel panelTitleBar;
        private Label lblTitleChildForm;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton1;
        private Label lblExit;
        private Label lblMinimize;
        private Label lblMaximize;
        private Label label1;
        private Label label2;
        private Label label3;
        private FontAwesome.Sharp.IconButton btnOutputFolder;
        private OpenFileDialog openFileDialogOutput;
        private Label label4;
        private FontAwesome.Sharp.IconButton btnAddressErrorScanner;
    }
}