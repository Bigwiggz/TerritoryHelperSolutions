namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    partial class FormGetTerritoryInformation
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
            label9 = new Label();
            label8 = new Label();
            label1 = new Label();
            label6 = new Label();
            label2 = new Label();
            btnSelectAddressFile = new FontAwesome.Sharp.IconButton();
            folderBrowserDialogSaveTerritoryHelperAddresses = new FolderBrowserDialog();
            label5 = new Label();
            label10 = new Label();
            btnRunGetTerritoryInformationScript = new FontAwesome.Sharp.IconButton();
            label7 = new Label();
            openFileDialogInput = new OpenFileDialog();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            btnGetTerritoriesList = new FontAwesome.Sharp.IconButton();
            label16 = new Label();
            openFileTerritoriesDialogInput = new OpenFileDialog();
            label3 = new Label();
            SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.White;
            label9.Location = new Point(12, 9);
            label9.Name = "label9";
            label9.Size = new Size(135, 32);
            label9.TabIndex = 41;
            label9.Text = "Description";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Coral;
            label8.Location = new Point(12, 52);
            label8.Name = "label8";
            label8.Size = new Size(441, 19);
            label8.TabIndex = 42;
            label8.Text = "This script is designed to save all territory records to a master excel file.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 176);
            label1.Name = "label1";
            label1.Size = new Size(71, 32);
            label1.TabIndex = 43;
            label1.Text = "Steps";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Coral;
            label6.Location = new Point(12, 262);
            label6.MaximumSize = new Size(600, 0);
            label6.Name = "label6";
            label6.Size = new Size(556, 38);
            label6.TabIndex = 51;
            label6.Text = "2) Open Territory Helper using the user name (email) and password and download all the addresses.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Coral;
            label2.Location = new Point(12, 218);
            label2.MaximumSize = new Size(600, 0);
            label2.Name = "label2";
            label2.Size = new Size(327, 19);
            label2.TabIndex = 53;
            label2.Text = "1) Check Configuration Settings first and save them.";
            // 
            // btnSelectAddressFile
            // 
            btnSelectAddressFile.FlatStyle = FlatStyle.Flat;
            btnSelectAddressFile.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectAddressFile.ForeColor = Color.Coral;
            btnSelectAddressFile.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnSelectAddressFile.IconColor = Color.Coral;
            btnSelectAddressFile.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnSelectAddressFile.IconSize = 28;
            btnSelectAddressFile.ImageAlign = ContentAlignment.MiddleLeft;
            btnSelectAddressFile.Location = new Point(20, 336);
            btnSelectAddressFile.Name = "btnSelectAddressFile";
            btnSelectAddressFile.Size = new Size(191, 33);
            btnSelectAddressFile.TabIndex = 4;
            btnSelectAddressFile.Text = "Get Address File";
            btnSelectAddressFile.TextAlign = ContentAlignment.MiddleLeft;
            btnSelectAddressFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectAddressFile.UseVisualStyleBackColor = true;
            btnSelectAddressFile.Click += btnSelectAddressFile_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Coral;
            label5.Location = new Point(12, 544);
            label5.MaximumSize = new Size(600, 0);
            label5.Name = "label5";
            label5.Size = new Size(392, 19);
            label5.TabIndex = 55;
            label5.Text = "4) Click the button below to save the excel master regsiter file. ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.Coral;
            label10.Location = new Point(12, 589);
            label10.MaximumSize = new Size(600, 0);
            label10.Name = "label10";
            label10.Size = new Size(600, 38);
            label10.TabIndex = 57;
            label10.Text = "NOTE: During the time that the script is running, PLEASE refrain from touching your computer as it can stop the script.";
            // 
            // btnRunGetTerritoryInformationScript
            // 
            btnRunGetTerritoryInformationScript.FlatStyle = FlatStyle.Flat;
            btnRunGetTerritoryInformationScript.ForeColor = Color.Coral;
            btnRunGetTerritoryInformationScript.IconChar = FontAwesome.Sharp.IconChar.Download;
            btnRunGetTerritoryInformationScript.IconColor = Color.Coral;
            btnRunGetTerritoryInformationScript.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRunGetTerritoryInformationScript.IconSize = 24;
            btnRunGetTerritoryInformationScript.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunGetTerritoryInformationScript.Location = new Point(143, 649);
            btnRunGetTerritoryInformationScript.Name = "btnRunGetTerritoryInformationScript";
            btnRunGetTerritoryInformationScript.Size = new Size(261, 64);
            btnRunGetTerritoryInformationScript.TabIndex = 6;
            btnRunGetTerritoryInformationScript.Text = "Run Get Territories";
            btnRunGetTerritoryInformationScript.TextAlign = ContentAlignment.MiddleLeft;
            btnRunGetTerritoryInformationScript.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRunGetTerritoryInformationScript.UseVisualStyleBackColor = true;
            btnRunGetTerritoryInformationScript.Click += btnRunGetTerritoryInformationScript_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Coral;
            label7.Location = new Point(12, 629);
            label7.MaximumSize = new Size(600, 0);
            label7.Name = "label7";
            label7.Size = new Size(0, 19);
            label7.TabIndex = 59;
            // 
            // openFileDialogInput
            // 
            openFileDialogInput.FileName = "SelectFile";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(12, 86);
            label11.Name = "label11";
            label11.Size = new Size(80, 32);
            label11.TabIndex = 61;
            label11.Text = "Inputs";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.White;
            label12.Location = new Point(298, 86);
            label12.Name = "label12";
            label12.Size = new Size(100, 32);
            label12.TabIndex = 62;
            label12.Text = "Outputs";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label13.ForeColor = Color.Coral;
            label13.Location = new Point(12, 118);
            label13.Name = "label13";
            label13.Size = new Size(176, 19);
            label13.TabIndex = 63;
            label13.Text = "•Territory Helper Addresses";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = Color.Coral;
            label14.Location = new Point(12, 137);
            label14.Name = "label14";
            label14.Size = new Size(174, 19);
            label14.TabIndex = 64;
            label14.Text = "•Territory Helper Territories";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = Color.Coral;
            label15.Location = new Point(298, 118);
            label15.Name = "label15";
            label15.Size = new Size(115, 19);
            label15.TabIndex = 65;
            label15.Text = "•Master Excel File";
            // 
            // btnGetTerritoriesList
            // 
            btnGetTerritoriesList.FlatStyle = FlatStyle.Flat;
            btnGetTerritoriesList.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnGetTerritoriesList.ForeColor = Color.Coral;
            btnGetTerritoriesList.IconChar = FontAwesome.Sharp.IconChar.FileCode;
            btnGetTerritoriesList.IconColor = Color.Coral;
            btnGetTerritoriesList.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnGetTerritoriesList.IconSize = 28;
            btnGetTerritoriesList.ImageAlign = ContentAlignment.MiddleLeft;
            btnGetTerritoriesList.Location = new Point(20, 487);
            btnGetTerritoriesList.Name = "btnGetTerritoriesList";
            btnGetTerritoriesList.Size = new Size(191, 33);
            btnGetTerritoriesList.TabIndex = 5;
            btnGetTerritoriesList.Text = "Get Territories List";
            btnGetTerritoriesList.TextAlign = ContentAlignment.MiddleLeft;
            btnGetTerritoriesList.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGetTerritoriesList.UseVisualStyleBackColor = true;
            btnGetTerritoriesList.Click += btnGetTerritoriesList_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label16.ForeColor = Color.Coral;
            label16.Location = new Point(12, 402);
            label16.MaximumSize = new Size(600, 0);
            label16.Name = "label16";
            label16.Size = new Size(569, 38);
            label16.TabIndex = 89;
            label16.Text = "3) Then download the Territory JSON file and log out.   Once you have downloaded the file, select it using the button below.";
            // 
            // openFileTerritoriesDialogInput
            // 
            openFileTerritoriesDialogInput.FileName = "openFileDialog1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Coral;
            label3.Location = new Point(-1, 884);
            label3.MaximumSize = new Size(600, 0);
            label3.Name = "label3";
            label3.Size = new Size(0, 19);
            label3.TabIndex = 91;
            // 
            // FormGetTerritoryInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(32, 30, 45);
            ClientSize = new Size(685, 482);
            Controls.Add(label3);
            Controls.Add(btnGetTerritoriesList);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label7);
            Controls.Add(btnRunGetTerritoryInformationScript);
            Controls.Add(label10);
            Controls.Add(label5);
            Controls.Add(btnSelectAddressFile);
            Controls.Add(label2);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(label8);
            Controls.Add(label9);
            Name = "FormGetTerritoryInformation";
            Text = "FormGetTerritoryInformation";
            Load += FormGetTerritoryInformation_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label9;
        private Label label8;
        private Label label1;
        private Label label6;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnSelectAddressFile;
        private FolderBrowserDialog folderBrowserDialogSaveTerritoryHelperAddresses;
        private Label label5;
        private Label label10;
        private FontAwesome.Sharp.IconButton btnRunGetTerritoryInformationScript;
        private Label label7;
        private OpenFileDialog openFileDialogInput;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private FontAwesome.Sharp.IconButton btnGetTerritoriesList;
        private Label label16;
        private OpenFileDialog openFileTerritoriesDialogInput;
        private Label label3;
    }
}