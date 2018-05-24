namespace CRM
{
    partial class frmImport_USER
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnChonFileUser = new DevComponents.DotNetBar.ButtonX();
            this.ofdChonFileUser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn file dữ liệu người sử dụng IPCAS";
            // 
            // btnChonFileUser
            // 
            this.btnChonFileUser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChonFileUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnChonFileUser.Location = new System.Drawing.Point(78, 86);
            this.btnChonFileUser.Name = "btnChonFileUser";
            this.btnChonFileUser.Size = new System.Drawing.Size(132, 31);
            this.btnChonFileUser.TabIndex = 1;
            this.btnChonFileUser.Text = "Chọn File";
            this.btnChonFileUser.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // ofdChonFileUser
            // 
            this.ofdChonFileUser.FileName = "openFileDialog1";
            // 
            // frmImport_USER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 160);
            this.Controls.Add(this.btnChonFileUser);
            this.Controls.Add(this.label1);
            this.Name = "frmImport_USER";
            this.Text = "frmImport_USER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnChonFileUser;
        private System.Windows.Forms.OpenFileDialog ofdChonFileUser;
    }
}