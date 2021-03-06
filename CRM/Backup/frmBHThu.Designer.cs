namespace CRM
{
    partial class frmBHThu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbLoaiKH = new System.Windows.Forms.GroupBox();
            this.optTen = new System.Windows.Forms.RadioButton();
            this.optMAKH = new System.Windows.Forms.RadioButton();
            this.optSCN = new System.Windows.Forms.RadioButton();
            this.optSHD = new System.Windows.Forms.RadioButton();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.txtTim = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvDanhsach = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.txtSotien = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtGhichu = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSoHD = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dgvDSThu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtHoahong = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.grbLoaiKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSThu)).BeginInit();
            this.SuspendLayout();
            // 
            // grbLoaiKH
            // 
            this.grbLoaiKH.Controls.Add(this.optTen);
            this.grbLoaiKH.Controls.Add(this.optMAKH);
            this.grbLoaiKH.Controls.Add(this.optSCN);
            this.grbLoaiKH.Controls.Add(this.optSHD);
            this.grbLoaiKH.Location = new System.Drawing.Point(21, 12);
            this.grbLoaiKH.Name = "grbLoaiKH";
            this.grbLoaiKH.Size = new System.Drawing.Size(706, 51);
            this.grbLoaiKH.TabIndex = 266;
            this.grbLoaiKH.TabStop = false;
            // 
            // optTen
            // 
            this.optTen.AutoSize = true;
            this.optTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTen.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optTen.Location = new System.Drawing.Point(439, 15);
            this.optTen.Name = "optTen";
            this.optTen.Size = new System.Drawing.Size(141, 24);
            this.optTen.TabIndex = 16;
            this.optTen.Text = "Tên khách hàng";
            this.optTen.UseVisualStyleBackColor = true;
            // 
            // optMAKH
            // 
            this.optMAKH.AutoSize = true;
            this.optMAKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optMAKH.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optMAKH.Location = new System.Drawing.Point(297, 15);
            this.optMAKH.Name = "optMAKH";
            this.optMAKH.Size = new System.Drawing.Size(136, 24);
            this.optMAKH.TabIndex = 15;
            this.optMAKH.Text = "Mã khách hàng";
            this.optMAKH.UseVisualStyleBackColor = true;
            // 
            // optSCN
            // 
            this.optSCN.AutoSize = true;
            this.optSCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSCN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optSCN.Location = new System.Drawing.Point(134, 15);
            this.optSCN.Name = "optSCN";
            this.optSCN.Size = new System.Drawing.Size(157, 24);
            this.optSCN.TabIndex = 14;
            this.optSCN.Text = "Số GCN Bảo hiểm";
            this.optSCN.UseVisualStyleBackColor = true;
            // 
            // optSHD
            // 
            this.optSHD.AutoSize = true;
            this.optSHD.Checked = true;
            this.optSHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSHD.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.optSHD.Location = new System.Drawing.Point(6, 15);
            this.optSHD.Name = "optSHD";
            this.optSHD.Size = new System.Drawing.Size(118, 24);
            this.optSHD.TabIndex = 13;
            this.optSHD.TabStop = true;
            this.optSHD.Text = "Số hợp đồng";
            this.optSHD.UseVisualStyleBackColor = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(215, 69);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(44, 23);
            this.buttonX1.TabIndex = 269;
            this.buttonX1.Text = "Tìm";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // txtTim
            // 
            // 
            // 
            // 
            this.txtTim.Border.Class = "TextBoxBorder";
            this.txtTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTim.Location = new System.Drawing.Point(21, 69);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(188, 26);
            this.txtTim.TabIndex = 7;
            // 
            // dgvDanhsach
            // 
            this.dgvDanhsach.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDanhsach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhsach.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhsach.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhsach.Location = new System.Drawing.Point(3, 134);
            this.dgvDanhsach.Name = "dgvDanhsach";
            this.dgvDanhsach.Size = new System.Drawing.Size(732, 140);
            this.dgvDanhsach.TabIndex = 271;
            this.dgvDanhsach.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellDoubleClick);
            this.dgvDanhsach.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhsach_CellContentClick);
            // 
            // labelX12
            // 
            this.labelX12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX12.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX12.Location = new System.Drawing.Point(3, 98);
            this.labelX12.Name = "labelX12";
            this.labelX12.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX12.Size = new System.Drawing.Size(732, 39);
            this.labelX12.TabIndex = 272;
            this.labelX12.Text = "DANH SÁCH HỢP ĐỒNG BẢO HIỂM";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dtpNgay
            // 
            this.dtpNgay.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpNgay.Enabled = false;
            this.dtpNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgay.Location = new System.Drawing.Point(107, 311);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.ShowUpDown = true;
            this.dtpNgay.Size = new System.Drawing.Size(103, 26);
            this.dtpNgay.TabIndex = 2;
            // 
            // txtSotien
            // 
            // 
            // 
            // 
            this.txtSotien.Border.Class = "TextBoxBorder";
            this.txtSotien.Enabled = false;
            this.txtSotien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(107, 341);
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(103, 27);
            this.txtSotien.TabIndex = 3;
            this.txtSotien.TextChanged += new System.EventHandler(this.txtSotien_TextChanged);
            this.txtSotien.Leave += new System.EventHandler(this.txtSotien_Leave);
            this.txtSotien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSotien_KeyPress);
            // 
            // txtGhichu
            // 
            // 
            // 
            // 
            this.txtGhichu.Border.Class = "TextBoxBorder";
            this.txtGhichu.Enabled = false;
            this.txtGhichu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhichu.Location = new System.Drawing.Point(105, 415);
            this.txtGhichu.Multiline = true;
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhichu.Size = new System.Drawing.Size(187, 54);
            this.txtGhichu.TabIndex = 5;
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(621, 461);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 23);
            this.btnDel.TabIndex = 316;
            this.btnDel.Text = "Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.Location = new System.Drawing.Point(522, 461);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(89, 23);
            this.btnModify.TabIndex = 315;
            this.btnModify.Text = "Lưu";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(426, 461);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 23);
            this.btnAdd.TabIndex = 314;
            this.btnAdd.Text = "Thu tiền";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX4
            // 
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(3, 311);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(98, 21);
            this.labelX4.TabIndex = 317;
            this.labelX4.Text = "Ngày thu";
            // 
            // labelX1
            // 
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(3, 342);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(98, 21);
            this.labelX1.TabIndex = 318;
            this.labelX1.Text = "Số tiền";
            // 
            // labelX2
            // 
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(3, 415);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(98, 21);
            this.labelX2.TabIndex = 319;
            this.labelX2.Text = "Ghi chú";
            // 
            // txtSoHD
            // 
            // 
            // 
            // 
            this.txtSoHD.Border.Class = "TextBoxBorder";
            this.txtSoHD.Enabled = false;
            this.txtSoHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHD.Location = new System.Drawing.Point(106, 279);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Size = new System.Drawing.Size(188, 26);
            this.txtSoHD.TabIndex = 1;
            // 
            // labelX3
            // 
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(3, 280);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(98, 21);
            this.labelX3.TabIndex = 321;
            this.labelX3.Text = "Số hợp đồng";
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelX5.Location = new System.Drawing.Point(312, 279);
            this.labelX5.Name = "labelX5";
            this.labelX5.SingleLineColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelX5.Size = new System.Drawing.Size(423, 39);
            this.labelX5.TabIndex = 322;
            this.labelX5.Text = "DANH SÁCH THU TIỀN BẢO HIỂM ";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dgvDSThu
            // 
            this.dgvDSThu.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDSThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSThu.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSThu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDSThu.Location = new System.Drawing.Point(312, 311);
            this.dgvDSThu.Name = "dgvDSThu";
            this.dgvDSThu.Size = new System.Drawing.Size(423, 144);
            this.dgvDSThu.TabIndex = 323;
            this.dgvDSThu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSThu_CellContentClick);
            // 
            // labelX6
            // 
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(3, 379);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(98, 21);
            this.labelX6.TabIndex = 325;
            this.labelX6.Text = "Hoa hồng";
            // 
            // txtHoahong
            // 
            // 
            // 
            // 
            this.txtHoahong.Border.Class = "TextBoxBorder";
            this.txtHoahong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoahong.Location = new System.Drawing.Point(107, 378);
            this.txtHoahong.Name = "txtHoahong";
            this.txtHoahong.Size = new System.Drawing.Size(185, 27);
            this.txtHoahong.TabIndex = 4;
            this.txtHoahong.TextChanged += new System.EventHandler(this.txtHoahong_TextChanged);
            // 
            // frmBHThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 494);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtHoahong);
            this.Controls.Add(this.dgvDSThu);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtSoHD);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtGhichu);
            this.Controls.Add(this.txtSotien);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.dgvDanhsach);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.grbLoaiKH);
            this.Name = "frmBHThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thu tien bao hiem";
            this.Load += new System.EventHandler(this.frmBHThu_Load);
            this.grbLoaiKH.ResumeLayout(false);
            this.grbLoaiKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhsach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbLoaiKH;
        private System.Windows.Forms.RadioButton optMAKH;
        private System.Windows.Forms.RadioButton optSCN;
        private System.Windows.Forms.RadioButton optSHD;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTim;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhsach;
        private DevComponents.DotNetBar.LabelX labelX12;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSotien;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGhichu;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private System.Windows.Forms.RadioButton optTen;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoHD;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDSThu;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHoahong;
    }
}