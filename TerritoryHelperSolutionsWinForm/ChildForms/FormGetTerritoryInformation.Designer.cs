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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGetTerritoryInformation));
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTerritoryHelperPassword = new System.Windows.Forms.Label();
            this.lblTerritoryHelperEmail = new System.Windows.Forms.Label();
            this.tbTerritoryHelperEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectAddressFile = new FontAwesome.Sharp.IconButton();
            this.folderBrowserDialogSaveTerritoryHelperAddresses = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRunGetTerritoryInformationScript = new FontAwesome.Sharp.IconButton();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
            this.mTBTerritoryHelperPassword = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 32);
            this.label9.TabIndex = 41;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.Coral;
            this.label8.Location = new System.Drawing.Point(12, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(441, 19);
            this.label8.TabIndex = 42;
            this.label8.Text = "This script is designed to save all territory records to a master excel file.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 32);
            this.label1.TabIndex = 43;
            this.label1.Text = "Steps";
            // 
            // lblTerritoryHelperPassword
            // 
            this.lblTerritoryHelperPassword.AutoSize = true;
            this.lblTerritoryHelperPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTerritoryHelperPassword.ForeColor = System.Drawing.Color.Coral;
            this.lblTerritoryHelperPassword.Location = new System.Drawing.Point(20, 362);
            this.lblTerritoryHelperPassword.Name = "lblTerritoryHelperPassword";
            this.lblTerritoryHelperPassword.Size = new System.Drawing.Size(166, 19);
            this.lblTerritoryHelperPassword.TabIndex = 48;
            this.lblTerritoryHelperPassword.Text = "Territory Helper Password";
            // 
            // lblTerritoryHelperEmail
            // 
            this.lblTerritoryHelperEmail.AutoSize = true;
            this.lblTerritoryHelperEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTerritoryHelperEmail.ForeColor = System.Drawing.Color.Coral;
            this.lblTerritoryHelperEmail.Location = new System.Drawing.Point(20, 330);
            this.lblTerritoryHelperEmail.Name = "lblTerritoryHelperEmail";
            this.lblTerritoryHelperEmail.Size = new System.Drawing.Size(140, 19);
            this.lblTerritoryHelperEmail.TabIndex = 46;
            this.lblTerritoryHelperEmail.Text = "Territory Helper Email";
            // 
            // tbTerritoryHelperEmail
            // 
            this.tbTerritoryHelperEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbTerritoryHelperEmail.Location = new System.Drawing.Point(197, 329);
            this.tbTerritoryHelperEmail.Name = "tbTerritoryHelperEmail";
            this.tbTerritoryHelperEmail.Size = new System.Drawing.Size(287, 23);
            this.tbTerritoryHelperEmail.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Coral;
            this.label3.Location = new System.Drawing.Point(12, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(567, 19);
            this.label3.TabIndex = 49;
            this.label3.Text = "NOTE: Multifactor Authentication MUST be turned OFF for the user account above fo" +
    "r now.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Coral;
            this.label6.Location = new System.Drawing.Point(12, 409);
            this.label6.MaximumSize = new System.Drawing.Size(600, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(561, 76);
            this.label6.TabIndex = 51;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Coral;
            this.label4.Location = new System.Drawing.Point(12, 260);
            this.label4.MaximumSize = new System.Drawing.Size(600, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(571, 38);
            this.label4.TabIndex = 52;
            this.label4.Text = "2) Put in your user name (email) and password to the main Territory Helper Accoun" +
    "t for the congregation below.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(12, 218);
            this.label2.MaximumSize = new System.Drawing.Size(600, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(327, 19);
            this.label2.TabIndex = 53;
            this.label2.Text = "1) Check Configuration Settings first and save them.";
            // 
            // btnSelectAddressFile
            // 
            this.btnSelectAddressFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAddressFile.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSelectAddressFile.ForeColor = System.Drawing.Color.Coral;
            this.btnSelectAddressFile.IconChar = FontAwesome.Sharp.IconChar.Folder;
            this.btnSelectAddressFile.IconColor = System.Drawing.Color.Coral;
            this.btnSelectAddressFile.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSelectAddressFile.IconSize = 28;
            this.btnSelectAddressFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAddressFile.Location = new System.Drawing.Point(20, 509);
            this.btnSelectAddressFile.Name = "btnSelectAddressFile";
            this.btnSelectAddressFile.Size = new System.Drawing.Size(156, 33);
            this.btnSelectAddressFile.TabIndex = 54;
            this.btnSelectAddressFile.Text = "Get Address File";
            this.btnSelectAddressFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAddressFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectAddressFile.UseVisualStyleBackColor = true;
            this.btnSelectAddressFile.Click += new System.EventHandler(this.btnSelectAddressFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Coral;
            this.label5.Location = new System.Drawing.Point(12, 567);
            this.label5.MaximumSize = new System.Drawing.Size(600, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(392, 19);
            this.label5.TabIndex = 55;
            this.label5.Text = "4) Click the button below to save the excel master regsiter file. ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.Coral;
            this.label10.Location = new System.Drawing.Point(12, 599);
            this.label10.MaximumSize = new System.Drawing.Size(600, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(600, 38);
            this.label10.TabIndex = 57;
            this.label10.Text = "NOTE: During the time that the script is running, PLEASE refrain from touching yo" +
    "ur computer as it can stop the script.";
            // 
            // btnRunGetTerritoryInformationScript
            // 
            this.btnRunGetTerritoryInformationScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunGetTerritoryInformationScript.ForeColor = System.Drawing.Color.Coral;
            this.btnRunGetTerritoryInformationScript.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.btnRunGetTerritoryInformationScript.IconColor = System.Drawing.Color.Coral;
            this.btnRunGetTerritoryInformationScript.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRunGetTerritoryInformationScript.IconSize = 24;
            this.btnRunGetTerritoryInformationScript.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunGetTerritoryInformationScript.Location = new System.Drawing.Point(213, 671);
            this.btnRunGetTerritoryInformationScript.Name = "btnRunGetTerritoryInformationScript";
            this.btnRunGetTerritoryInformationScript.Size = new System.Drawing.Size(203, 48);
            this.btnRunGetTerritoryInformationScript.TabIndex = 58;
            this.btnRunGetTerritoryInformationScript.Text = "Run Get Territories";
            this.btnRunGetTerritoryInformationScript.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunGetTerritoryInformationScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRunGetTerritoryInformationScript.UseVisualStyleBackColor = true;
            this.btnRunGetTerritoryInformationScript.Click += new System.EventHandler(this.btnRunGetTerritoryInformationScript_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Coral;
            this.label7.Location = new System.Drawing.Point(12, 629);
            this.label7.MaximumSize = new System.Drawing.Size(600, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 19);
            this.label7.TabIndex = 59;
            // 
            // openFileDialogInput
            // 
            this.openFileDialogInput.FileName = "SelectFile";
            // 
            // mTBTerritoryHelperPassword
            // 
            this.mTBTerritoryHelperPassword.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mTBTerritoryHelperPassword.Location = new System.Drawing.Point(197, 362);
            this.mTBTerritoryHelperPassword.Name = "mTBTerritoryHelperPassword";
            this.mTBTerritoryHelperPassword.PasswordChar = '*';
            this.mTBTerritoryHelperPassword.Size = new System.Drawing.Size(287, 22);
            this.mTBTerritoryHelperPassword.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(12, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 32);
            this.label11.TabIndex = 61;
            this.label11.Text = "Inputs";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(298, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 32);
            this.label12.TabIndex = 62;
            this.label12.Text = "Outputs";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.Coral;
            this.label13.Location = new System.Drawing.Point(12, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(176, 19);
            this.label13.TabIndex = 63;
            this.label13.Text = "•Territory Helper Addresses";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.Coral;
            this.label14.Location = new System.Drawing.Point(12, 137);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(174, 19);
            this.label14.TabIndex = 64;
            this.label14.Text = "•Territory Helper Territories";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.Coral;
            this.label15.Location = new System.Drawing.Point(298, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 19);
            this.label15.TabIndex = 65;
            this.label15.Text = "•Master Excel File";
            // 
            // FormGetTerritoryInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(668, 482);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.mTBTerritoryHelperPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnRunGetTerritoryInformationScript);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSelectAddressFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTerritoryHelperPassword);
            this.Controls.Add(this.lblTerritoryHelperEmail);
            this.Controls.Add(this.tbTerritoryHelperEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Name = "FormGetTerritoryInformation";
            this.Text = "FormGetTerritoryInformation";
            this.Load += new System.EventHandler(this.FormGetTerritoryInformation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label9;
        private Label label8;
        private Label label1;
        private Label lblTerritoryHelperPassword;
        private Label lblTerritoryHelperEmail;
        private TextBox tbTerritoryHelperEmail;
        private Label label3;
        private Label label6;
        private Label label4;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnSelectAddressFile;
        private FolderBrowserDialog folderBrowserDialogSaveTerritoryHelperAddresses;
        private Label label5;
        private Label label10;
        private FontAwesome.Sharp.IconButton btnRunGetTerritoryInformationScript;
        private Label label7;
        private OpenFileDialog openFileDialogInput;
        private MaskedTextBox mTBTerritoryHelperPassword;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
    }
}