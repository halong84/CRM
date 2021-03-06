namespace CRM
{
    partial class frmLichchamsoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnKH = new DevComponents.DotNetBar.ButtonX();
            this.cbbNV = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.dtpNgayKT = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayBD = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtNoiDung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtChiphi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cbTieuchi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.cbLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblt = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.grbLoaiKH = new System.Windows.Forms.GroupBox();
            this.optLDDN = new System.Windows.Forms.RadioButton();
            this.optDN = new System.Windows.Forms.RadioButton();
            this.optCN = new System.Windows.Forms.RadioButton();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTongKP = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.cbQuy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            this.grbLoaiKH.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKH
            // 
            this.btnKH.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKH.Location = new System.Drawing.Point(447, 187);
            this.btnKH.Name = "btnKH";
            this.btnKH.Size = new System.Drawing.Size(114, 23);
            this.btnKH.TabIndex = 183;
            this.btnKH.Text = "Chi tiết KH";
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);
            // 
            // cbbNV
            // 
            this.cbbNV.DisplayMember = "Text";
            this.cbbNV.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbNV.FormattingEnabled = true;
            this.cbbNV.ItemHeight = 19;
            this.cbbNV.Location = new System.Drawing.Point(368, 313);
            this.cbbNV.Name = "cbbNV";
            this.cbbNV.Size = new System.Drawing.Size(231, 25);
            this.cbbNV.TabIndex = 184;
            this.cbbNV.Visible = false;
            // 
            // labelX6
            // 
            this.labelX6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(179, 10);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(296, 23);
            this.labelX6.TabIndex = 198;
            this.labelX6.Text = "KẾ HOẠCH CHĂM SÓC KHÁCH HÀNG";
            // 
            // dtpNgayKT
            // 
            this.dtpNgayKT.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKT.Location = new System.Drawing.Point(555, 68);
            this.dtpNgayKT.Name = "dtpNgayKT";
            this.dtpNgayKT.ShowUpDown = true;
            this.dtpNgayKT.Size = new System.Drawing.Size(112, 26);
            this.dtpNgayKT.TabIndex = 186;
            this.dtpNgayKT.Value = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            // 
            // dtpNgayBD
            // 
            this.dtpNgayBD.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBD.Location = new System.Drawing.Point(555, 39);
            this.dtpNgayBD.Name = "dtpNgayBD";
            this.dtpNgayBD.ShowUpDown = true;
            this.dtpNgayBD.Size = new System.Drawing.Size(112, 26);
            this.dtpNgayBD.TabIndex = 185;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Location = new System.Drawing.Point(610, 469);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 190;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(514, 469);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 23);
            this.btnDel.TabIndex = 191;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Location = new System.Drawing.Point(567, 187);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(90, 23);
            this.btnModify.TabIndex = 189;
            this.btnModify.Text = "Lưu";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Location = new System.Drawing.Point(663, 187);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 23);
            this.btnAdd.TabIndex = 188;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(2, 255);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(757, 208);
            this.dgvDanhsach.TabIndex = 192;
            this.dgvDanhsach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellClick);
            this.dgvDanhsach.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellContentClick);
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(12, 126);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(98, 21);
            this.labelX4.TabIndex = 197;
            this.labelX4.Text = "Đối tượng KH";
            // 
            // txtNoiDung
            // 
            // 
            // 
            // 
            this.txtNoiDung.Border.Class = "TextBoxBorder";
            this.txtNoiDung.Location = new System.Drawing.Point(510, 97);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.Size = new System.Drawing.Size(249, 78);
            this.txtNoiDung.TabIndex = 187;
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(418, 97);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(68, 23);
            this.labelX5.TabIndex = 195;
            this.labelX5.Text = "Nội dung";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(418, 68);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(134, 23);
            this.labelX2.TabIndex = 193;
            this.labelX2.Text = "Thời gian kết thúc";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(418, 39);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(134, 21);
            this.labelX1.TabIndex = 194;
            this.labelX1.Text = "Thời gian bắt đầu";
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(268, 315);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(94, 23);
            this.labelX3.TabIndex = 196;
            this.labelX3.Text = "CB thực hiện";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            this.labelX3.Visible = false;
            // 
            // txtChiphi
            // 
            // 
            // 
            // 
            this.txtChiphi.Border.Class = "TextBoxBorder";
            this.txtChiphi.Location = new System.Drawing.Point(110, 154);
            this.txtChiphi.Name = "txtChiphi";
            this.txtChiphi.Size = new System.Drawing.Size(85, 26);
            this.txtChiphi.TabIndex = 199;
            this.txtChiphi.TextChanged += new System.EventHandler(this.txtChiphi_TextChanged);
            this.txtChiphi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiphi_KeyPress);
            // 
            // labelX7
            // 
            this.labelX7.Location = new System.Drawing.Point(12, 153);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(61, 23);
            this.labelX7.TabIndex = 195;
            this.labelX7.Text = "Kinh phí";
            // 
            // labelX8
            // 
            this.labelX8.Location = new System.Drawing.Point(12, 97);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(61, 23);
            this.labelX8.TabIndex = 196;
            this.labelX8.Text = "Sự kiện:";
            // 
            // cbTieuchi
            // 
            this.cbTieuchi.DisplayMember = "Text";
            this.cbTieuchi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTieuchi.FormattingEnabled = true;
            this.cbTieuchi.ItemHeight = 19;
            this.cbTieuchi.Location = new System.Drawing.Point(79, 97);
            this.cbTieuchi.Name = "cbTieuchi";
            this.cbTieuchi.Size = new System.Drawing.Size(183, 25);
            this.cbTieuchi.TabIndex = 184;
            // 
            // labelX9
            // 
            this.labelX9.Location = new System.Drawing.Point(179, 156);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(14, 23);
            this.labelX9.TabIndex = 195;
            this.labelX9.Text = "đ";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX10
            // 
            this.labelX10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX10.Location = new System.Drawing.Point(12, 48);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(34, 20);
            this.labelX10.TabIndex = 238;
            this.labelX10.Text = "Quý:";
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(151, 42);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.ShowUpDown = true;
            this.dtpThang.Size = new System.Drawing.Size(61, 26);
            this.dtpThang.TabIndex = 237;

            this.dtpThang.Leave += new System.EventHandler(this.dtpThang_Leave);
            this.dtpThang.DropDown += new System.EventHandler(this.dtpThang_DropDown);
            // 
            // cbLoaiKH
            // 
            this.cbLoaiKH.DisplayMember = "Text";
            this.cbLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiKH.FormattingEnabled = true;
            this.cbLoaiKH.ItemHeight = 19;
            this.cbLoaiKH.Location = new System.Drawing.Point(110, 127);
            this.cbLoaiKH.Name = "cbLoaiKH";
            this.cbLoaiKH.Size = new System.Drawing.Size(152, 25);
            this.cbLoaiKH.TabIndex = 239;
            // 
            // lblt
            // 
            this.lblt.Enabled = false;
            this.lblt.Location = new System.Drawing.Point(12, 182);
            this.lblt.Name = "lblt";
            this.lblt.Size = new System.Drawing.Size(250, 28);
            this.lblt.TabIndex = 240;
            this.lblt.Text = "Tổng kinh phí:";
            // 
            // labelX12
            // 
            this.labelX12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX12.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX12.Location = new System.Drawing.Point(1, 216);
            this.labelX12.Name = "labelX12";
            this.labelX12.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX12.Size = new System.Drawing.Size(758, 39);
            this.labelX12.TabIndex = 257;
            this.labelX12.Text = "DANH SÁCH KẾ HOẠCH";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // grbLoaiKH
            // 
            this.grbLoaiKH.Controls.Add(this.optLDDN);
            this.grbLoaiKH.Controls.Add(this.optDN);
            this.grbLoaiKH.Controls.Add(this.optCN);
            this.grbLoaiKH.Location = new System.Drawing.Point(268, 29);
            this.grbLoaiKH.Name = "grbLoaiKH";
            this.grbLoaiKH.Size = new System.Drawing.Size(141, 91);
            this.grbLoaiKH.TabIndex = 265;
            this.grbLoaiKH.TabStop = false;
            // 
            // optLDDN
            // 
            this.optLDDN.AutoSize = true;
            this.optLDDN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optLDDN.Location = new System.Drawing.Point(6, 61);
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
            this.optDN.Location = new System.Drawing.Point(6, 37);
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
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(283, 149);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(105, 26);
            this.txtMa.TabIndex = 266;
            this.txtMa.Visible = false;
            // 
            // lblTongKP
            // 
            this.lblTongKP.Enabled = false;
            this.lblTongKP.Location = new System.Drawing.Point(128, 182);
            this.lblTongKP.Name = "lblTongKP";
            this.lblTongKP.Size = new System.Drawing.Size(190, 28);
            this.lblTongKP.TabIndex = 267;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Location = new System.Drawing.Point(218, 42);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(44, 23);
            this.buttonX1.TabIndex = 268;
            this.buttonX1.Text = "Tìm";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX11
            // 
            this.labelX11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX11.Location = new System.Drawing.Point(110, 47);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(39, 20);
            this.labelX11.TabIndex = 269;
            this.labelX11.Text = "Năm:";
            // 
            // cbQuy
            // 
            this.cbQuy.DisplayMember = "Text";
            this.cbQuy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbQuy.FormattingEnabled = true;
            this.cbQuy.ItemHeight = 19;
            this.cbQuy.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.cbQuy.Location = new System.Drawing.Point(52, 42);
            this.cbQuy.Name = "cbQuy";
            this.cbQuy.Size = new System.Drawing.Size(44, 25);
            this.cbQuy.TabIndex = 270;
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
            // frmLichchamsoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 504);
            this.Controls.Add(this.cbQuy);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.lblTongKP);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.grbLoaiKH);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.lblt);
            this.Controls.Add(this.cbLoaiKH);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.txtChiphi);
            this.Controls.Add(this.btnKH);
            this.Controls.Add(this.cbTieuchi);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.dtpNgayKT);
            this.Controls.Add(this.dtpNgayBD);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cbbNV);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLichchamsoc";
            this.Text = "Ke hoach cham soc khach hang";
            this.Load += new System.EventHandler(this.frmLichchamsoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            this.grbLoaiKH.ResumeLayout(false);
            this.grbLoaiKH.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnKH;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbNV;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.DateTimePicker dtpNgayKT;
        private System.Windows.Forms.DateTimePicker dtpNgayBD;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNoiDung;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtChiphi;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTieuchi;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.LabelX labelX10;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLoaiKH;
        private DevComponents.DotNetBar.LabelX lblt;
        private DevComponents.DotNetBar.LabelX labelX12;
        private System.Windows.Forms.GroupBox grbLoaiKH;
        private System.Windows.Forms.RadioButton optDN;
        private System.Windows.Forms.RadioButton optCN;
        private System.Windows.Forms.TextBox txtMa;
        private DevComponents.DotNetBar.LabelX lblTongKP;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbQuy;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private System.Windows.Forms.RadioButton optLDDN;


    }
}