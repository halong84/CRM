namespace CRM
{
    partial class frmHT_Thamso
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtTenCN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMaCN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtDDImport = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtDT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtDiaChi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(116, 23);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(50, 24);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Mã CN";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(109, 56);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(57, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "Tên CN";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTenCN
            // 
            // 
            // 
            // 
            this.txtTenCN.Border.Class = "TextBoxBorder";
            this.txtTenCN.Location = new System.Drawing.Point(186, 53);
            this.txtTenCN.Name = "txtTenCN";
            this.txtTenCN.Size = new System.Drawing.Size(323, 26);
            this.txtTenCN.TabIndex = 2;
            // 
            // txtMaCN
            // 
            // 
            // 
            // 
            this.txtMaCN.Border.Class = "TextBoxBorder";
            this.txtMaCN.Location = new System.Drawing.Point(186, 21);
            this.txtMaCN.Name = "txtMaCN";
            this.txtMaCN.Size = new System.Drawing.Size(71, 26);
            this.txtMaCN.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Location = new System.Drawing.Point(355, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(436, 182);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtDDImport
            // 
            // 
            // 
            // 
            this.txtDDImport.Border.Class = "TextBoxBorder";
            this.txtDDImport.Location = new System.Drawing.Point(185, 140);
            this.txtDDImport.Name = "txtDDImport";
            this.txtDDImport.Size = new System.Drawing.Size(323, 26);
            this.txtDDImport.TabIndex = 6;
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(12, 143);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(173, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "Thư mục import dữ liệu:";
            // 
            // txtDT
            // 
            // 
            // 
            // 
            this.txtDT.Border.Class = "TextBoxBorder";
            this.txtDT.Location = new System.Drawing.Point(186, 82);
            this.txtDT.Name = "txtDT";
            this.txtDT.Size = new System.Drawing.Size(323, 26);
            this.txtDT.TabIndex = 8;
            this.txtDT.TextChanged += new System.EventHandler(this.textBoxX1_TextChanged);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(69, 85);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(106, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "Số điện thoại:";
            this.labelX4.Click += new System.EventHandler(this.labelX4_Click);
            // 
            // txtDiaChi
            // 
            // 
            // 
            // 
            this.txtDiaChi.Border.Class = "TextBoxBorder";
            this.txtDiaChi.Location = new System.Drawing.Point(186, 111);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(323, 26);
            this.txtDiaChi.TabIndex = 10;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(109, 114);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(66, 23);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "Điạ chỉ:";
            // 
            // frmHT_Thamso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 216);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtDT);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtDDImport);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMaCN);
            this.Controls.Add(this.txtTenCN);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHT_Thamso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tham so he thong";
            this.Load += new System.EventHandler(this.frmThamso_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHT_Thamso_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenCN;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMaCN;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDDImport;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDT;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDiaChi;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}