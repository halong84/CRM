namespace CRM
{
    partial class frmTK_KeHoach
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.grbLoaiKH = new System.Windows.Forms.GroupBox();
            this.optLDDN = new System.Windows.Forms.RadioButton();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbTieuchi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbCN = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.comboItem16 = new DevComponents.Editors.ComboItem();
            this.comboItem17 = new DevComponents.Editors.ComboItem();
            this.comboItem18 = new DevComponents.Editors.ComboItem();
            this.comboItem19 = new DevComponents.Editors.ComboItem();
            this.comboItem20 = new DevComponents.Editors.ComboItem();
            this.comboItem21 = new DevComponents.Editors.ComboItem();
            this.comboItem22 = new DevComponents.Editors.ComboItem();
            this.comboItem23 = new DevComponents.Editors.ComboItem();
            this.comboItem24 = new DevComponents.Editors.ComboItem();
            this.comboItem39 = new DevComponents.Editors.ComboItem();
            this.comboItem40 = new DevComponents.Editors.ComboItem();
            this.comboItem41 = new DevComponents.Editors.ComboItem();
            this.comboItem42 = new DevComponents.Editors.ComboItem();
            this.comboItem75 = new DevComponents.Editors.ComboItem();
            this.comboItem76 = new DevComponents.Editors.ComboItem();
            this.cbQuy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.grbLoaiKH.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(133, 157);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(87, 25);
            this.buttonX1.TabIndex = 363;
            this.buttonX1.Text = "Thống kê";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // grbLoaiKH
            // 
            this.grbLoaiKH.Controls.Add(this.optLDDN);
            this.grbLoaiKH.Controls.Add(this.optDN);
            this.grbLoaiKH.Controls.Add(this.optCN);
            this.grbLoaiKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLoaiKH.Location = new System.Drawing.Point(234, 0);
            this.grbLoaiKH.Name = "grbLoaiKH";
            this.grbLoaiKH.Size = new System.Drawing.Size(132, 92);
            this.grbLoaiKH.TabIndex = 366;
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
            this.optDN.Click += new System.EventHandler(this.optDN_Click);
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
            // cbLoaiKH
            // 
            this.cbLoaiKH.DisplayMember = "Text";
            this.cbLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiKH.FormattingEnabled = true;
            this.cbLoaiKH.ItemHeight = 19;
            this.cbLoaiKH.Location = new System.Drawing.Point(133, 125);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(168, 25);
            this.cbLoaiKH.TabIndex = 370;
            // 
            // cbTieuchi
            // 
            this.cbTieuchi.DisplayMember = "Text";
            this.cbTieuchi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTieuchi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTieuchi.FormattingEnabled = true;
            this.cbTieuchi.ItemHeight = 19;
            this.cbTieuchi.Location = new System.Drawing.Point(133, 96);
            this.cbTieuchi.Name = "cbTieuchi";
            this.cbTieuchi.Size = new System.Drawing.Size(168, 25);
            this.cbTieuchi.TabIndex = 367;
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(12, 125);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(97, 21);
            this.labelX4.TabIndex = 369;
            this.labelX4.Text = "Đối tượng KH";
            // 
            // labelX8
            // 
            this.labelX8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.Location = new System.Drawing.Point(12, 96);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(61, 23);
            this.labelX8.TabIndex = 368;
            this.labelX8.Text = "Sự kiện:";
            // 
            // labelX12
            // 
            this.labelX12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Location = new System.Drawing.Point(12, 17);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 22);
            this.labelX12.TabIndex = 372;
            this.labelX12.Text = "Chi nhánh :";
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
            this.cbCN.Items.AddRange(new object[] {
            this.comboItem15,
            this.comboItem16,
            this.comboItem17,
            this.comboItem18,
            this.comboItem19,
            this.comboItem20,
            this.comboItem21,
            this.comboItem22,
            this.comboItem23,
            this.comboItem24,
            this.comboItem39,
            this.comboItem40,
            this.comboItem41,
            this.comboItem42,
            this.comboItem75,
            this.comboItem76});
            this.cbCN.Location = new System.Drawing.Point(106, 14);
            this.cbCN.Name = "cbCN";
            this.cbCN.Size = new System.Drawing.Size(115, 25);
            this.cbCN.TabIndex = 371;
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "Tất cả";
            // 
            // comboItem16
            // 
            this.comboItem16.Text = "4800";
            // 
            // comboItem17
            // 
            this.comboItem17.Text = "4801";
            // 
            // comboItem18
            // 
            this.comboItem18.Text = "4802";
            // 
            // comboItem19
            // 
            this.comboItem19.Text = "4803";
            // 
            // comboItem20
            // 
            this.comboItem20.Text = "4804";
            // 
            // comboItem21
            // 
            this.comboItem21.Text = "4805";
            // 
            // comboItem22
            // 
            this.comboItem22.Text = "4806";
            // 
            // comboItem23
            // 
            this.comboItem23.Text = "4807";
            // 
            // comboItem24
            // 
            this.comboItem24.Text = "4808";
            // 
            // comboItem39
            // 
            this.comboItem39.Text = "4809";
            // 
            // comboItem40
            // 
            this.comboItem40.Text = "4810";
            // 
            // comboItem41
            // 
            this.comboItem41.Text = "4811";
            // 
            // comboItem42
            // 
            this.comboItem42.Text = "4813";
            // 
            // comboItem75
            // 
            this.comboItem75.Text = "4814";
            // 
            // comboItem76
            // 
            this.comboItem76.Text = "4815";
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
            this.cbQuy.Location = new System.Drawing.Point(64, 42);
            this.cbQuy.Name = "cbQuy";
            this.cbQuy.Size = new System.Drawing.Size(45, 25);
            this.cbQuy.TabIndex = 376;
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
            this.labelX11.Location = new System.Drawing.Point(115, 47);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(39, 20);
            this.labelX11.TabIndex = 375;
            this.labelX11.Text = "Năm:";
            // 
            // labelX10
            // 
            this.labelX10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX10.Location = new System.Drawing.Point(12, 48);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(34, 20);
            this.labelX10.TabIndex = 374;
            this.labelX10.Text = "Quý:";
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(160, 42);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(61, 26);
            this.dtpThang.TabIndex = 373;
            // 
            // frmTK_KeHoach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 200);
            this.Controls.Add(this.cbQuy);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.cbCN);
            this.Controls.Add(this.cbLoaiKH);
            this.Controls.Add(this.cbTieuchi);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.grbLoaiKH);
            this.Controls.Add(this.buttonX1);
            this.Name = "frmTK_KeHoach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTK_KeHoach";
            this.Load += new System.EventHandler(this.frmTK_KeHoach_Load);
            this.grbLoaiKH.ResumeLayout(false);
            this.grbLoaiKH.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.GroupBox grbLoaiKH;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.RadioButton optCN;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTieuchi;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCN;
        private DevComponents.Editors.ComboItem comboItem15;
        private DevComponents.Editors.ComboItem comboItem16;
        private DevComponents.Editors.ComboItem comboItem17;
        private DevComponents.Editors.ComboItem comboItem18;
        private DevComponents.Editors.ComboItem comboItem19;
        private DevComponents.Editors.ComboItem comboItem20;
        private DevComponents.Editors.ComboItem comboItem21;
        private DevComponents.Editors.ComboItem comboItem22;
        private DevComponents.Editors.ComboItem comboItem23;
        private DevComponents.Editors.ComboItem comboItem24;
        private DevComponents.Editors.ComboItem comboItem39;
        private DevComponents.Editors.ComboItem comboItem40;
        private DevComponents.Editors.ComboItem comboItem41;
        private DevComponents.Editors.ComboItem comboItem42;
        private DevComponents.Editors.ComboItem comboItem75;
        private DevComponents.Editors.ComboItem comboItem76;
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