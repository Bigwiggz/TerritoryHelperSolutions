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
            this.label9 = new System.Windows.Forms.Label();
            this.btnAccessProgressForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(218, 32);
            this.label9.TabIndex = 41;
            this.label9.Text = "Help Configuration";
            // 
            // btnAccessProgressForm
            // 
            this.btnAccessProgressForm.Location = new System.Drawing.Point(115, 121);
            this.btnAccessProgressForm.Name = "btnAccessProgressForm";
            this.btnAccessProgressForm.Size = new System.Drawing.Size(247, 57);
            this.btnAccessProgressForm.TabIndex = 42;
            this.btnAccessProgressForm.Text = "Access Progress Form";
            this.btnAccessProgressForm.UseVisualStyleBackColor = true;
            this.btnAccessProgressForm.Click += new System.EventHandler(this.btnAccessProgressForm_Click);
            // 
            // FormHelpConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(668, 482);
            this.Controls.Add(this.btnAccessProgressForm);
            this.Controls.Add(this.label9);
            this.Name = "FormHelpConfiguration";
            this.Text = "FormHelpConfiguration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label9;
        private Button btnAccessProgressForm;
    }
}