namespace CRM
{
    partial class frmHT_ImportKH
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChonfileKH = new DevComponents.DotNetBar.ButtonX();
            this.ofdNhapfileKH = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(49, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 37);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // btnChonfileKH
            // 
            this.btnChonfileKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChonfileKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnChonfileKH.Location = new System.Drawing.Point(124, 35);
            this.btnChonfileKH.Name = "btnChonfileKH";
            this.btnChonfileKH.Size = new System.Drawing.Size(75, 23);
            this.btnChonfileKH.TabIndex = 208;
            this.btnChonfileKH.Text = "Chọn file";
            this.btnChonfileKH.Click += new System.EventHandler(this.btnChonfileKH_Click);
            // 
            // ofdNhapfileKH
            // 
            this.ofdNhapfileKH.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 20);
            this.label2.TabIndex = 209;
            this.label2.Text = "Chọn file Thông tin khách hàng cần nhập";
            // 
            // frmHT_ImportKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 130);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnChonfileKH);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmHT_ImportKH";
            this.ShowIcon = false;
            this.Text = "Nhập thông tin khách hàng";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX btnChonfileKH;
        private System.Windows.Forms.OpenFileDialog ofdNhapfileKH;
        private System.Windows.Forms.Label label2;
    }
}