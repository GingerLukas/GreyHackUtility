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
            this.optimizeButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.beforeLabel = new System.Windows.Forms.Label();
            this.afterLabel = new System.Windows.Forms.Label();
            this.rationLabel = new System.Windows.Forms.Label();
            this.swapButton = new System.Windows.Forms.Button();
            this.includeButton = new System.Windows.Forms.Button();
            this._btnSelectLocalClassesFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputRichTextBox.Location = new System.Drawing.Point(502, 10);
            this.outputRichTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(375, 309);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            this.outputRichTextBox.WordWrap = false;
            // 
            // inputRichTextBox
            // 
            this.inputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputRichTextBox.Location = new System.Drawing.Point(9, 10);
            this.inputRichTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputRichTextBox.Name = "inputRichTextBox";
            this.inputRichTextBox.Size = new System.Drawing.Size(375, 309);
            this.inputRichTextBox.TabIndex = 0;
            this.inputRichTextBox.Text = "";
            this.inputRichTextBox.WordWrap = false;
            // 
            // optimizeButton
            // 
            this.optimizeButton.Location = new System.Drawing.Point(388, 266);
            this.optimizeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.optimizeButton.Name = "optimizeButton";
            this.optimizeButton.Size = new System.Drawing.Size(109, 52);
            this.optimizeButton.TabIndex = 2;
            this.optimizeButton.Text = "Optimize";
            this.optimizeButton.UseVisualStyleBackColor = true;
            this.optimizeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(395, 321);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(93, 13);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "Last optimize time:";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // beforeLabel
            // 
            this.beforeLabel.AutoSize = true;
            this.beforeLabel.Location = new System.Drawing.Point(119, 321);
            this.beforeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.beforeLabel.Name = "beforeLabel";
            this.beforeLabel.Size = new System.Drawing.Size(37, 13);
            this.beforeLabel.TabIndex = 4;
            this.beforeLabel.Text = "before";
            this.beforeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // afterLabel
            // 
            this.afterLabel.AutoSize = true;
            this.afterLabel.Location = new System.Drawing.Point(836, 321);
            this.afterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.afterLabel.Name = "afterLabel";
            this.afterLabel.Size = new System.Drawing.Size(28, 13);
            this.afterLabel.TabIndex = 5;
            this.afterLabel.Text = "after";
            this.afterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rationLabel
            // 
            this.rationLabel.AutoSize = true;
            this.rationLabel.Location = new System.Drawing.Point(440, 250);
            this.rationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rationLabel.Name = "rationLabel";
            this.rationLabel.Size = new System.Drawing.Size(15, 13);
            this.rationLabel.TabIndex = 6;
            this.rationLabel.Text = "%";
            this.rationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // swapButton
            // 
            this.swapButton.Location = new System.Drawing.Point(388, 10);
            this.swapButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.swapButton.Name = "swapButton";
            this.swapButton.Size = new System.Drawing.Size(109, 52);
            this.swapButton.TabIndex = 7;
            this.swapButton.Text = "<<";
            this.swapButton.UseVisualStyleBackColor = true;
            this.swapButton.Click += new System.EventHandler(this.swapButton_Click);
            // 
            // includeButton
            // 
            this.includeButton.Location = new System.Drawing.Point(388, 186);
            this.includeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.includeButton.Name = "includeButton";
            this.includeButton.Size = new System.Drawing.Size(109, 48);
            this.includeButton.TabIndex = 8;
            this.includeButton.Text = "Include";
            this.includeButton.UseVisualStyleBackColor = true;
            this.includeButton.Click += new System.EventHandler(this.includeButton_Click);
            // 
            // _btnSelectLocalClassesFolder
            // 
            this._btnSelectLocalClassesFolder.Location = new System.Drawing.Point(388, 100);
            this._btnSelectLocalClassesFolder.Name = "_btnSelectLocalClassesFolder";
            this._btnSelectLocalClassesFolder.Size = new System.Drawing.Size(109, 43);
            this._btnSelectLocalClassesFolder.TabIndex = 9;
            this._btnSelectLocalClassesFolder.Text = "Change local directory for classes";
            this._btnSelectLocalClassesFolder.UseVisualStyleBackColor = true;
            this._btnSelectLocalClassesFolder.Click += new System.EventHandler(this._btnSelectLocalClassesFolder_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 344);
            this.Controls.Add(this._btnSelectLocalClassesFolder);
            this.Controls.Add(this.includeButton);
            this.Controls.Add(this.swapButton);
            this.Controls.Add(this.rationLabel);
            this.Controls.Add(this.afterLabel);
            this.Controls.Add(this.beforeLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.optimizeButton);
            this.Controls.Add(this.inputRichTextBox);
            this.Controls.Add(this.outputRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GreyHack Optimizer by Ginger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.Button optimizeButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label beforeLabel;
        private System.Windows.Forms.Label afterLabel;
        private System.Windows.Forms.Label rationLabel;
        private System.Windows.Forms.RichTextBox inputRichTextBox;
        private System.Windows.Forms.Button swapButton;
        private System.Windows.Forms.Button includeButton;
        private System.Windows.Forms.Button _btnSelectLocalClassesFolder;
    }
}

