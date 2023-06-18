namespace TerritoryHelperSolutionsWinForm.ChildForms
{
    partial class FormHelpConfiguration
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
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
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
            label9.Size = new Size(71, 32);
            label9.TabIndex = 41;
            label9.Text = "Help ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Coral;
            label4.Location = new Point(12, 59);
            label4.MaximumSize = new Size(600, 0);
            label4.Name = "label4";
            label4.Size = new Size(99, 19);
            label4.TabIndex = 75;
            label4.Text = "VERSION 1.0.0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(12, 78);
            label1.MaximumSize = new Size(600, 0);
            label1.Name = "label1";
            label1.Size = new Size(256, 19);
            label1.TabIndex = 76;
            label1.Text = "-Added 4 main scripts and configuration";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Coral;
            label2.Location = new Point(12, 116);
            label2.MaximumSize = new Size(600, 0);
            label2.Name = "label2";
            label2.Size = new Size(358, 19);
            label2.TabIndex = 77;
            label2.Text = "-Removed user name/password requirement for 2 scripts";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Coral;
            label3.Location = new Point(12, 97);
            label3.MaximumSize = new Size(600, 0);
            label3.Name = "label3";
            label3.Size = new Size(99, 19);
            label3.TabIndex = 78;
            label3.Text = "VERSION 1.0.1";
            // 
            // FormHelpConfiguration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 30, 45);
            ClientSize = new Size(668, 482);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(label9);
            Name = "FormHelpConfiguration";
            Text = "FormHelpConfiguration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label9;
        private Label label4;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}