namespace TerritoryHelperSolutionsWinForm.UtilityForms
{
    partial class FormProgressBar
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblWorkLabel = new System.Windows.Forms.Label();
            this.lblTotalElapsedTime = new System.Windows.Forms.Label();
            this.timerElapsedTime = new System.Windows.Forms.Timer(this.components);
            this.openFileDialogOutput = new System.Windows.Forms.OpenFileDialog();
            this.lblSubTaskMessage = new System.Windows.Forms.Label();
            this.lblSubTaskProcessing = new System.Windows.Forms.Label();
            this.progressBarSubTask = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(37, 94);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(581, 23);
            this.progressBar.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(271, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(116, 21);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Processing...0%";
            // 
            // lblWorkLabel
            // 
            this.lblWorkLabel.AutoSize = true;
            this.lblWorkLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWorkLabel.Location = new System.Drawing.Point(37, 55);
            this.lblWorkLabel.Name = "lblWorkLabel";
            this.lblWorkLabel.Size = new System.Drawing.Size(61, 15);
            this.lblWorkLabel.TabIndex = 3;
            this.lblWorkLabel.Text = "Running...";
            // 
            // lblTotalElapsedTime
            // 
            this.lblTotalElapsedTime.AutoSize = true;
            this.lblTotalElapsedTime.Location = new System.Drawing.Point(494, 252);
            this.lblTotalElapsedTime.Name = "lblTotalElapsedTime";
            this.lblTotalElapsedTime.Size = new System.Drawing.Size(124, 15);
            this.lblTotalElapsedTime.TabIndex = 4;
            this.lblTotalElapsedTime.Text = "Time Elapsed: 00:00:00";
            // 
            // timerElapsedTime
            // 
            this.timerElapsedTime.Enabled = true;
            this.timerElapsedTime.Tick += new System.EventHandler(this.timerElapsedTime_Tick);
            // 
            // openFileDialogOutput
            // 
            this.openFileDialogOutput.FileName = "openFileDialog1";
            // 
            // lblSubTaskMessage
            // 
            this.lblSubTaskMessage.AutoSize = true;
            this.lblSubTaskMessage.Location = new System.Drawing.Point(37, 168);
            this.lblSubTaskMessage.Name = "lblSubTaskMessage";
            this.lblSubTaskMessage.Size = new System.Drawing.Size(109, 15);
            this.lblSubTaskMessage.TabIndex = 7;
            this.lblSubTaskMessage.Text = "Sub Task Running...";
            // 
            // lblSubTaskProcessing
            // 
            this.lblSubTaskProcessing.AutoSize = true;
            this.lblSubTaskProcessing.Location = new System.Drawing.Point(273, 147);
            this.lblSubTaskProcessing.Name = "lblSubTaskProcessing";
            this.lblSubTaskProcessing.Size = new System.Drawing.Size(114, 15);
            this.lblSubTaskProcessing.TabIndex = 6;
            this.lblSubTaskProcessing.Text = "Sub-Processing...0%";
            // 
            // progressBarSubTask
            // 
            this.progressBarSubTask.Location = new System.Drawing.Point(37, 201);
            this.progressBarSubTask.Name = "progressBarSubTask";
            this.progressBarSubTask.Size = new System.Drawing.Size(581, 23);
            this.progressBarSubTask.TabIndex = 5;
            // 
            // FormProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(658, 276);
            this.Controls.Add(this.lblSubTaskMessage);
            this.Controls.Add(this.lblSubTaskProcessing);
            this.Controls.Add(this.progressBarSubTask);
            this.Controls.Add(this.lblTotalElapsedTime);
            this.Controls.Add(this.lblWorkLabel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.ForeColor = System.Drawing.Color.Coral;
            this.Name = "FormProgressBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Script Task Runner";
            this.Load += new System.EventHandler(this.FormProgressBar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar progressBar;
        private Label lblStatus;
        private Label lblWorkLabel;
        private Label lblTotalElapsedTime;
        private System.Windows.Forms.Timer timerElapsedTime;
        private OpenFileDialog openFileDialogOutput;
        private Label lblSubTaskMessage;
        private Label lblSubTaskProcessing;
        private ProgressBar progressBarSubTask;
    }
}