namespace CRM
{
    partial class frmNhapKH_TuFile
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
            this.btnChonFileKH = new DevComponents.DotNetBar.ButtonX();
            this.ofdNhapfileKH = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnChonFileKH
            // 
            this.btnChonFileKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChonFileKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnChonFileKH.Location = new System.Drawing.Point(45, 30);
            this.btnChonFileKH.Name = "btnChonFileKH";
            this.btnChonFileKH.Size = new System.Drawing.Size(243, 30);
            this.btnChonFileKH.TabIndex = 0;
            this.btnChonFileKH.Text = "Chọn file thông tin khách hàng";
            this.btnChonFileKH.Click += new System.EventHandler(this.btnChonFileKH_Click);
            // 
            // ofdNhapfileKH
            // 
            this.ofdNhapfileKH.FileName = "openFileDialog1";
            // 
            // frmNhapKH_TuFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 90);
            this.Controls.Add(this.btnChonFileKH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmNhapKH_TuFile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập thông tin khách hàng theo ngày";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnChonFileKH;
        private System.Windows.Forms.OpenFileDialog ofdNhapfileKH;
    }
}