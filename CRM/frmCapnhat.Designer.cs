namespace CRM
{
    partial class frmCapnhat
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
            this.lblPhienbanMoi = new DevComponents.DotNetBar.LabelX();
            this.lblPhienbanHT = new DevComponents.DotNetBar.LabelX();
            this.btnCapnhat = new DevComponents.DotNetBar.ButtonX();
            this.lblOldVer = new DevComponents.DotNetBar.LabelX();
            this.lblNewVer = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // lblPhienbanMoi
            // 
            this.lblPhienbanMoi.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhienbanMoi.Location = new System.Drawing.Point(53, 50);
            this.lblPhienbanMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPhienbanMoi.Name = "lblPhienbanMoi";
            this.lblPhienbanMoi.Size = new System.Drawing.Size(134, 26);
            this.lblPhienbanMoi.TabIndex = 11;
            this.lblPhienbanMoi.Text = "Phiên bản mới:";
            this.lblPhienbanMoi.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // lblPhienbanHT
            // 
            this.lblPhienbanHT.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhienbanHT.Location = new System.Drawing.Point(53, 14);
            this.lblPhienbanHT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPhienbanHT.Name = "lblPhienbanHT";
            this.lblPhienbanHT.Size = new System.Drawing.Size(162, 26);
            this.lblPhienbanHT.TabIndex = 10;
            this.lblPhienbanHT.Text = "Phiên bản hiện tại:";
            this.lblPhienbanHT.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnCapnhat
            // 
            this.btnCapnhat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapnhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapnhat.Location = new System.Drawing.Point(126, 106);
            this.btnCapnhat.Name = "btnCapnhat";
            this.btnCapnhat.Size = new System.Drawing.Size(89, 33);
            this.btnCapnhat.TabIndex = 13;
            this.btnCapnhat.Text = "Tải về";
            this.btnCapnhat.Click += new System.EventHandler(this.btnCapnhat_Click);
            // 
            // lblOldVer
            // 
            this.lblOldVer.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldVer.Location = new System.Drawing.Point(223, 14);
            this.lblOldVer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblOldVer.Name = "lblOldVer";
            this.lblOldVer.Size = new System.Drawing.Size(79, 26);
            this.lblOldVer.TabIndex = 10;
            this.lblOldVer.Text = "Cũ";
            // 
            // lblNewVer
            // 
            this.lblNewVer.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVer.Location = new System.Drawing.Point(223, 50);
            this.lblNewVer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(79, 26);
            this.lblNewVer.TabIndex = 10;
            this.lblNewVer.Text = "Mới";
            // 
            // frmCapnhat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 151);
            this.Controls.Add(this.btnCapnhat);
            this.Controls.Add(this.lblPhienbanMoi);
            this.Controls.Add(this.lblNewVer);
            this.Controls.Add(this.lblOldVer);
            this.Controls.Add(this.lblPhienbanHT);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmCapnhat";
            this.Text = "Cap nhat phien ban";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblPhienbanMoi;
        private DevComponents.DotNetBar.LabelX lblPhienbanHT;
        private DevComponents.DotNetBar.ButtonX btnCapnhat;
        private DevComponents.DotNetBar.LabelX lblOldVer;
        private DevComponents.DotNetBar.LabelX lblNewVer;



    }
}