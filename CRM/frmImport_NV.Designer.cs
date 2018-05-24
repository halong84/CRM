namespace CRM
{
    partial class frmImport_NV
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
            this.btnChonFileNV = new DevComponents.DotNetBar.ButtonX();
            this.ofdChonFileNV = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(58, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn file dữ liệu nhân viên cần nhập";
            // 
            // btnChonFileNV
            // 
            this.btnChonFileNV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChonFileNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnChonFileNV.Location = new System.Drawing.Point(117, 79);
            this.btnChonFileNV.Name = "btnChonFileNV";
            this.btnChonFileNV.Size = new System.Drawing.Size(122, 32);
            this.btnChonFileNV.TabIndex = 1;
            this.btnChonFileNV.Text = "Chọn File";
            this.btnChonFileNV.Click += new System.EventHandler(this.btnChonFileNV_Click);
            // 
            // ofdChonFileNV
            // 
            this.ofdChonFileNV.FileName = "openFileDialog1";
            // 
            // frmImport_NV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 130);
            this.Controls.Add(this.btnChonFileNV);
            this.Controls.Add(this.label1);
            this.Name = "frmImport_NV";
            this.ShowIcon = false;
            this.Text = "Nhập thông tin nhân viên Agribank";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnChonFileNV;
        private System.Windows.Forms.OpenFileDialog ofdChonFileNV;
    }
}