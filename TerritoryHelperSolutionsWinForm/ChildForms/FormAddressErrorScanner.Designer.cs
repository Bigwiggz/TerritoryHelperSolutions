namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    partial class FormAddressErrorScanner
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
            label15 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label1 = new Label();
            label8 = new Label();
            label9 = new Label();
            btnSelectAddressFile = new FontAwesome.Sharp.IconButton();
            label6 = new Label();
            openFileDialogInput = new OpenFileDialog();
            btnRunAddressErrorScannerScript = new FontAwesome.Sharp.IconButton();
            label10 = new Label();
            SuspendLayout();
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = Color.Coral;
            label15.Location = new Point(298, 118);
            label15.Name = "label15";
            label15.Size = new Size(115, 19);
            label15.TabIndex = 74;
            label15.Text = "•Master Excel File";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label13.ForeColor = Color.Coral;
            label13.Location = new Point(12, 118);
            label13.Name = "label13";
            label13.Size = new Size(176, 19);
            label13.TabIndex = 72;
            label13.Text = "•Territory Helper Addresses";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.White;
            label12.Location = new Point(298, 86);
            label12.Name = "label12";
            label12.Size = new Size(100, 32);
            label12.TabIndex = 71;
            label12.Text = "Outputs";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(12, 86);
            label11.Name = "label11";
            label11.Size = new Size(80, 32);
            label11.TabIndex = 70;
            label11.Text = "Inputs";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 176);
            label1.Name = "label1";
            label1.Size = new Size(71, 32);
            label1.TabIndex = 68;
            label1.Text = "Steps";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.Coral;
            label8.Location = new Point(12, 52);
            label8.Name = "label8";
            label8.Size = new Size(441, 19);
            label8.TabIndex = 67;
            label8.Text = "This script is designed to save all territory records to a master excel file.";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.White;
            label9.Location = new Point(12, 9);
            label9.Name = "label9";
            label9.Size = new Size(135, 32);
            label9.TabIndex = 66;
            label9.Text = "Description";
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
            btnSelectAddressFile.Location = new Point(20, 294);
            btnSelectAddressFile.Name = "btnSelectAddressFile";
            btnSelectAddressFile.Size = new Size(191, 33);
            btnSelectAddressFile.TabIndex = 75;
            btnSelectAddressFile.Text = "Get Address File";
            btnSelectAddressFile.TextAlign = ContentAlignment.MiddleLeft;
            btnSelectAddressFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSelectAddressFile.UseVisualStyleBackColor = true;
            btnSelectAddressFile.Click += btnSelectAddressFile_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Coral;
            label6.Location = new Point(12, 220);
            label6.MaximumSize = new Size(600, 0);
            label6.Name = "label6";
            label6.Size = new Size(556, 38);
            label6.TabIndex = 76;
            label6.Text = "3) Open Territory Helper using the user name (email) and password and download all the addresses.";
            // 
            // openFileDialogInput
            // 
            openFileDialogInput.FileName = "openFileDialogInput";
            // 
            // btnRunAddressErrorScannerScript
            // 
            btnRunAddressErrorScannerScript.FlatStyle = FlatStyle.Flat;
            btnRunAddressErrorScannerScript.ForeColor = Color.Coral;
            btnRunAddressErrorScannerScript.IconChar = FontAwesome.Sharp.IconChar.Download;
            btnRunAddressErrorScannerScript.IconColor = Color.Coral;
            btnRunAddressErrorScannerScript.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRunAddressErrorScannerScript.IconSize = 24;
            btnRunAddressErrorScannerScript.ImageAlign = ContentAlignment.MiddleLeft;
            btnRunAddressErrorScannerScript.Location = new Point(151, 412);
            btnRunAddressErrorScannerScript.Name = "btnRunAddressErrorScannerScript";
            btnRunAddressErrorScannerScript.Size = new Size(261, 64);
            btnRunAddressErrorScannerScript.TabIndex = 77;
            btnRunAddressErrorScannerScript.Text = "Run Address Error Scanner";
            btnRunAddressErrorScannerScript.TextAlign = ContentAlignment.MiddleLeft;
            btnRunAddressErrorScannerScript.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRunAddressErrorScannerScript.UseVisualStyleBackColor = true;
            btnRunAddressErrorScannerScript.Click += btnRunAddressErrorScannerScript_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.Coral;
            label10.Location = new Point(20, 352);
            label10.MaximumSize = new Size(600, 0);
            label10.Name = "label10";
            label10.Size = new Size(600, 38);
            label10.TabIndex = 78;
            label10.Text = "NOTE: During the time that the script is running, PLEASE refrain from touching your computer as it can stop the script.";
            // 
            // FormAddressErrorScanner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 30, 45);
            ClientSize = new Size(668, 482);
            Controls.Add(btnRunAddressErrorScannerScript);
            Controls.Add(label10);
            Controls.Add(btnSelectAddressFile);
            Controls.Add(label6);
            Controls.Add(label15);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label1);
            Controls.Add(label8);
            Controls.Add(label9);
            Name = "FormAddressErrorScanner";
            Text = "AddressErrorScanner";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label15;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label1;
        private Label label8;
        private Label label9;
        private FontAwesome.Sharp.IconButton btnSelectAddressFile;
        private Label label6;
        private OpenFileDialog openFileDialogInput;
        private FontAwesome.Sharp.IconButton btnRunAddressErrorScannerScript;
        private Label label10;
    }
}