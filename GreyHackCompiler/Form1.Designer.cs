namespace GreyHackCompiler
{
    partial class Form1
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
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.inputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.mainButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.beforeLabel = new System.Windows.Forms.Label();
            this.afterLabel = new System.Windows.Forms.Label();
            this.rationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputRichTextBox.Location = new System.Drawing.Point(670, 12);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(500, 380);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            // 
            // inputRichTextBox
            // 
            this.inputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.inputRichTextBox.Name = "inputRichTextBox";
            this.inputRichTextBox.Size = new System.Drawing.Size(500, 380);
            this.inputRichTextBox.TabIndex = 0;
            this.inputRichTextBox.Text = "";
            // 
            // mainButton
            // 
            this.mainButton.Location = new System.Drawing.Point(518, 328);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(145, 64);
            this.mainButton.TabIndex = 2;
            this.mainButton.Text = "Optimize";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(527, 395);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(125, 17);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "Last optimize time:";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // beforeLabel
            // 
            this.beforeLabel.AutoSize = true;
            this.beforeLabel.Location = new System.Drawing.Point(159, 395);
            this.beforeLabel.Name = "beforeLabel";
            this.beforeLabel.Size = new System.Drawing.Size(49, 17);
            this.beforeLabel.TabIndex = 4;
            this.beforeLabel.Text = "before";
            this.beforeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // afterLabel
            // 
            this.afterLabel.AutoSize = true;
            this.afterLabel.Location = new System.Drawing.Point(1115, 395);
            this.afterLabel.Name = "afterLabel";
            this.afterLabel.Size = new System.Drawing.Size(37, 17);
            this.afterLabel.TabIndex = 5;
            this.afterLabel.Text = "after";
            this.afterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rationLabel
            // 
            this.rationLabel.AutoSize = true;
            this.rationLabel.Location = new System.Drawing.Point(587, 308);
            this.rationLabel.Name = "rationLabel";
            this.rationLabel.Size = new System.Drawing.Size(20, 17);
            this.rationLabel.TabIndex = 6;
            this.rationLabel.Text = "%";
            this.rationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 423);
            this.Controls.Add(this.rationLabel);
            this.Controls.Add(this.afterLabel);
            this.Controls.Add(this.beforeLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.mainButton);
            this.Controls.Add(this.inputRichTextBox);
            this.Controls.Add(this.outputRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GreyHack Optimizer by Ginger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label beforeLabel;
        private System.Windows.Forms.Label afterLabel;
        private System.Windows.Forms.Label rationLabel;
        private System.Windows.Forms.RichTextBox inputRichTextBox;
    }
}

