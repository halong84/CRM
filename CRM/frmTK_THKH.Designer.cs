namespace CRM
{
    partial class frmTK_THKH
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
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbTieuchi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.grbLoaiKH = new System.Windows.Forms.GroupBox();
            this.optLDDN = new System.Windows.Forms.RadioButton();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cbKeHoach = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.optTatca = new System.Windows.Forms.RadioButton();
            this.optChuanhan = new System.Windows.Forms.RadioButton();
            this.optNhan = new System.Windows.Forms.RadioButton();
            this.cbQuy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.grbLoaiKH.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCN
            // 
            this.cbCN.DisplayMember = "Text";
            this.cbCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCN.DropDownHeight = 200;
            this.cbCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCN.FormattingEnabled = true;
            this.cbCN.IntegralHeight = false;
            this.cbCN.ItemHeight = 19;
            this.cbCN.Location = new System.Drawing.Point(106, 9);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(436, 25);
            this.cbCN.TabIndex = 381;
            this.cbCN.TextChanged += new System.EventHandler(this.cbCN_TextChanged);
            // 
            // optDN
            // 
            this.optDN.AutoSize = true;
            this.optDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optDN.Location = new System.Drawing.Point(6, 42);
            this.optDN.Name = "optDN";
            this.optDN.Size = new System.Drawing.Size(127, 24);
            this.optDN.TabIndex = 14;
            this.optDN.Text = "Doanh nghiệp";
            this.optDN.UseVisualStyleBackColor = true;
            this.optDN.CheckedChanged += new System.EventHandler(this.optDN_CheckedChanged);
            this.optDN.Click += new System.EventHandler(this.optDN_Click);
            // 
            // cbLoaiKH
            // 
            this.cbLoaiKH.DisplayMember = "Text";
            this.cbLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiKH.FormattingEnabled = true;
            this.cbLoaiKH.ItemHeight = 19;
            this.cbLoaiKH.Location = new System.Drawing.Point(287, 115);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(168, 25);
            this.cbLoaiKH.TabIndex = 380;
            this.cbLoaiKH.TextChanged += new System.EventHandler(this.cbLoaiKH_TextChanged);
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(12, 12);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 382;
            this.labelX12.Text = "Chi nhánh :";
            // 
            // cbTieuchi
            // 
            this.cbTieuchi.DisplayMember = "Text";
            this.cbTieuchi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTieuchi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTieuchi.FormattingEnabled = true;
            this.cbTieuchi.ItemHeight = 19;
            this.cbTieuchi.Location = new System.Drawing.Point(287, 86);
            this.cbTieuchi.Name = "cbTieuchi";
            this.cbTieuchi.Size = new System.Drawing.Size(168, 25);
            this.cbTieuchi.TabIndex = 377;
            this.cbTieuchi.TextChanged += new System.EventHandler(this.cbTieuchi_TextChanged);
            // 
            // optCN
            // 
            this.optCN.AutoSize = true;
            this.optCN.Checked = true;
            this.optCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optCN.Location = new System.Drawing.Point(6, 15);
            this.optCN.Name = "optCN";
            this.optCN.Size = new System.Drawing.Size(87, 24);
            this.optCN.TabIndex = 13;
            this.optCN.TabStop = true;
            this.optCN.Text = "Cá nhân";
            this.optCN.UseVisualStyleBackColor = true;
            this.optCN.Click += new System.EventHandler(this.optCN_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(166, 115);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(97, 21);
            this.labelX4.TabIndex = 379;
            this.labelX4.Text = "Đối tượng KH";
            // 
            // grbLoaiKH
            // 
            this.grbLoaiKH.Controls.Add(this.optLDDN);
            this.grbLoaiKH.Controls.Add(this.optDN);
            this.grbLoaiKH.Controls.Add(this.optCN);
            this.grbLoaiKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLoaiKH.Location = new System.Drawing.Point(12, 70);
            this.grbLoaiKH.Name = "grbLoaiKH";
            this.grbLoaiKH.Size = new System.Drawing.Size(140, 97);
            this.grbLoaiKH.TabIndex = 376;
            this.grbLoaiKH.TabStop = false;
            // 
            // optLDDN
            // 
            this.optLDDN.AutoSize = true;
            this.optLDDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optLDDN.Location = new System.Drawing.Point(6, 67);
            this.optLDDN.Name = "optLDDN";
            this.optLDDN.Size = new System.Drawing.Size(121, 24);
            this.optLDDN.TabIndex = 15;
            this.optLDDN.Text = "Lãnh đạo DN";
            this.optLDDN.UseVisualStyleBackColor = true;
            this.optLDDN.Click += new System.EventHandler(this.optLDDN_Click);
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.Location = new System.Drawing.Point(166, 86);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(61, 23);
            this.labelX8.TabIndex = 378;
            this.labelX8.Text = "Sự kiện:";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(224, 223);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(87, 25);
            this.buttonX1.TabIndex = 373;
            this.buttonX1.Text = "Thống kê";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // cbKeHoach
            // 
            this.cbKeHoach.DisplayMember = "Text";
            this.cbKeHoach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbKeHoach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKeHoach.FormattingEnabled = true;
            this.cbKeHoach.ItemHeight = 19;
            this.cbKeHoach.Location = new System.Drawing.Point(287, 142);
            this.cbKeHoach.Name = "cbKeHoach";
            this.cbKeHoach.Size = new System.Drawing.Size(241, 25);
            this.cbKeHoach.TabIndex = 384;
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(166, 142);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(97, 21);
            this.labelX1.TabIndex = 383;
            this.labelX1.Text = "Kế hoạch";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.optTatca);
            this.groupBox10.Controls.Add(this.optChuanhan);
            this.groupBox10.Controls.Add(this.optNhan);
            this.groupBox10.Location = new System.Drawing.Point(12, 173);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(362, 44);
            this.groupBox10.TabIndex = 385;
            this.groupBox10.TabStop = false;
            // 
            // optTatca
            // 
            this.optTatca.AutoSize = true;
            this.optTatca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTatca.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optTatca.Location = new System.Drawing.Point(278, 14);
            this.optTatca.Name = "optTatca";
            this.optTatca.Size = new System.Drawing.Size(71, 24);
            this.optTatca.TabIndex = 15;
            this.optTatca.Text = "Tất cả";
            this.optTatca.UseVisualStyleBackColor = true;
            // 
            // optChuanhan
            // 
            this.optChuanhan.AutoSize = true;
            this.optChuanhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optChuanhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optChuanhan.Location = new System.Drawing.Point(140, 14);
            this.optChuanhan.Name = "optChuanhan";
            this.optChuanhan.Size = new System.Drawing.Size(136, 24);
            this.optChuanhan.TabIndex = 14;
            this.optChuanhan.Text = "Chưa nhận quà";
            this.optChuanhan.UseVisualStyleBackColor = true;
            // 
            // optNhan
            // 
            this.optNhan.AutoSize = true;
            this.optNhan.Checked = true;
            this.optNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optNhan.Location = new System.Drawing.Point(15, 14);
            this.optNhan.Name = "optNhan";
            this.optNhan.Size = new System.Drawing.Size(119, 24);
            this.optNhan.TabIndex = 13;
            this.optNhan.TabStop = true;
            this.optNhan.Text = "Đã nhận quà";
            this.optNhan.UseVisualStyleBackColor = true;
            // 
            // cbQuy
            // 
            this.cbQuy.DisplayMember = "Text";
            this.cbQuy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbQuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQuy.FormattingEnabled = true;
            this.cbQuy.ItemHeight = 19;
            this.cbQuy.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.cbQuy.Location = new System.Drawing.Point(106, 39);
            this.cbQuy.Name = "cbQuy";
            this.cbQuy.Size = new System.Drawing.Size(44, 25);
            this.cbQuy.TabIndex = 389;
            this.cbQuy.Text = "01";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "01";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "02";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "03";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "04";
            // 
            // labelX11
            // 
            this.labelX11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX11.Location = new System.Drawing.Point(166, 44);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(39, 20);
            this.labelX11.TabIndex = 388;
            this.labelX11.Text = "Năm:";
            // 
            // labelX10
            // 
            this.labelX10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX10.Location = new System.Drawing.Point(12, 44);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(34, 20);
            this.labelX10.TabIndex = 387;
            this.labelX10.Text = "Quý:";
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(224, 39);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(61, 26);
            this.dtpThang.TabIndex = 386;
            // 
            // frmTK_THKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 255);
            this.Controls.Add(this.cbQuy);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.cbKeHoach);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.cbLoaiKH);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbTieuchi);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.grbLoaiKH);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.buttonX1);
            this.Name = "frmTK_THKH";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê kết quả thực hiện kế hoạch";
            this.Load += new System.EventHandler(this.frmTK_THKH_Load);
            this.grbLoaiKH.ResumeLayout(false);
            this.grbLoaiKH.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private System.Windows.Forms.RadioButton optDN;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTieuchi;
        private System.Windows.Forms.RadioButton optCN;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.GroupBox grbLoaiKH;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbKeHoach;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton optChuanhan;
        private System.Windows.Forms.RadioButton optNhan;
        private System.Windows.Forms.RadioButton optTatca;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbQuy;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX10;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.RadioButton optLDDN;
    }
}