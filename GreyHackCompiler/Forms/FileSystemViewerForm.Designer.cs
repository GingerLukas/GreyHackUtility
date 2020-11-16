namespace GreyHackCompiler.Forms
{
    partial class FileSystemUtilityForm
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
            this._tvFileSystem = new System.Windows.Forms.TreeView();
            this._tvIPs = new System.Windows.Forms.TreeView();
            this._lblDescPublicIp = new System.Windows.Forms.Label();
            this._lblDescLocalIp = new System.Windows.Forms.Label();
            this._lblPublicIp = new System.Windows.Forms.Label();
            this._lblLocalIp = new System.Windows.Forms.Label();
            this._lbUsers = new System.Windows.Forms.ListBox();
            this._lblDescUsers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _tvFileSystem
            // 
            this._tvFileSystem.Location = new System.Drawing.Point(508, 12);
            this._tvFileSystem.Name = "_tvFileSystem";
            this._tvFileSystem.Size = new System.Drawing.Size(280, 428);
            this._tvFileSystem.TabIndex = 0;
            this._tvFileSystem.DoubleClick += new System.EventHandler(this._tvFileSystem_DoubleClick);
            // 
            // _tvIPs
            // 
            this._tvIPs.Location = new System.Drawing.Point(12, 12);
            this._tvIPs.Name = "_tvIPs";
            this._tvIPs.Size = new System.Drawing.Size(222, 428);
            this._tvIPs.TabIndex = 1;
            this._tvIPs.DoubleClick += new System.EventHandler(this._tvIPs_DoubleClick);
            // 
            // _lblDescPublicIp
            // 
            this._lblDescPublicIp.AutoSize = true;
            this._lblDescPublicIp.Location = new System.Drawing.Point(240, 12);
            this._lblDescPublicIp.Name = "_lblDescPublicIp";
            this._lblDescPublicIp.Size = new System.Drawing.Size(52, 13);
            this._lblDescPublicIp.TabIndex = 2;
            this._lblDescPublicIp.Text = "Public IP:";
            // 
            // _lblDescLocalIp
            // 
            this._lblDescLocalIp.AutoSize = true;
            this._lblDescLocalIp.Location = new System.Drawing.Point(240, 35);
            this._lblDescLocalIp.Name = "_lblDescLocalIp";
            this._lblDescLocalIp.Size = new System.Drawing.Size(49, 13);
            this._lblDescLocalIp.TabIndex = 3;
            this._lblDescLocalIp.Text = "Local IP:";
            // 
            // _lblPublicIp
            // 
            this._lblPublicIp.AutoSize = true;
            this._lblPublicIp.Location = new System.Drawing.Point(298, 12);
            this._lblPublicIp.Name = "_lblPublicIp";
            this._lblPublicIp.Size = new System.Drawing.Size(35, 13);
            this._lblPublicIp.TabIndex = 4;
            this._lblPublicIp.Text = "label1";
            // 
            // _lblLocalIp
            // 
            this._lblLocalIp.AutoSize = true;
            this._lblLocalIp.Location = new System.Drawing.Point(298, 35);
            this._lblLocalIp.Name = "_lblLocalIp";
            this._lblLocalIp.Size = new System.Drawing.Size(35, 13);
            this._lblLocalIp.TabIndex = 5;
            this._lblLocalIp.Text = "label2";
            // 
            // _lbUsers
            // 
            this._lbUsers.FormattingEnabled = true;
            this._lbUsers.Location = new System.Drawing.Point(243, 111);
            this._lbUsers.Name = "_lbUsers";
            this._lbUsers.Size = new System.Drawing.Size(120, 329);
            this._lbUsers.TabIndex = 6;
            this._lbUsers.SelectedIndexChanged += new System.EventHandler(this._lbUsers_SelectedIndexChanged);
            // 
            // _lblDescUsers
            // 
            this._lblDescUsers.AutoSize = true;
            this._lblDescUsers.Location = new System.Drawing.Point(240, 95);
            this._lblDescUsers.Name = "_lblDescUsers";
            this._lblDescUsers.Size = new System.Drawing.Size(37, 13);
            this._lblDescUsers.TabIndex = 7;
            this._lblDescUsers.Text = "Users:";
            // 
            // FileSystemUtilityForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._lblDescUsers);
            this.Controls.Add(this._lbUsers);
            this.Controls.Add(this._lblLocalIp);
            this.Controls.Add(this._lblPublicIp);
            this.Controls.Add(this._lblDescLocalIp);
            this.Controls.Add(this._lblDescPublicIp);
            this.Controls.Add(this._tvIPs);
            this.Controls.Add(this._tvFileSystem);
            this.Name = "FileSystemUtilityForm";
            this.Text = "FileSystemViewerForm";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileSystemUtilityForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileSystemUtilityForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView _tvFileSystem;
        private System.Windows.Forms.TreeView _tvIPs;
        private System.Windows.Forms.Label _lblDescPublicIp;
        private System.Windows.Forms.Label _lblDescLocalIp;
        private System.Windows.Forms.Label _lblPublicIp;
        private System.Windows.Forms.Label _lblLocalIp;
        private System.Windows.Forms.ListBox _lbUsers;
        private System.Windows.Forms.Label _lblDescUsers;
    }
}