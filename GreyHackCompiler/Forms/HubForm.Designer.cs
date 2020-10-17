namespace GreyHackCompiler.Forms
{
    partial class HubForm
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
            this._btnCompiler = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._btnFileSystemUtility = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnCompiler
            // 
            this._btnCompiler.Location = new System.Drawing.Point(3, 3);
            this._btnCompiler.Name = "_btnCompiler";
            this._btnCompiler.Size = new System.Drawing.Size(87, 44);
            this._btnCompiler.TabIndex = 0;
            this._btnCompiler.Text = "Compiler";
            this._btnCompiler.UseVisualStyleBackColor = true;
            this._btnCompiler.Click += new System.EventHandler(this._btnCompiler_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._btnCompiler);
            this.flowLayoutPanel1.Controls.Add(this._btnFileSystemUtility);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(776, 426);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // _btnFileSystemUtility
            // 
            this._btnFileSystemUtility.Location = new System.Drawing.Point(96, 3);
            this._btnFileSystemUtility.Name = "_btnFileSystemUtility";
            this._btnFileSystemUtility.Size = new System.Drawing.Size(87, 44);
            this._btnFileSystemUtility.TabIndex = 1;
            this._btnFileSystemUtility.Text = "FileSystem Utility";
            this._btnFileSystemUtility.UseVisualStyleBackColor = true;
            this._btnFileSystemUtility.Click += new System.EventHandler(this._btnFileSystemUtility_Click);
            // 
            // HubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "HubForm";
            this.Text = "HubForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnCompiler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _btnFileSystemUtility;
    }
}